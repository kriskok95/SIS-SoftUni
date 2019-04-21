using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;
using SIS.MvcFramework.Services;
using SIS.MvcFramework.Services.Contracts;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {
        protected Controller()
        {
            this.UserCookieService = new UserCookieService();
            this.Response = new HttpResponse();
            this.Response.StatusCode = HttpResponseStatusCode.Ok;
        }

        public IHttpRequest Request { get; set; }

        public IHttpResponse Response { get; set; }

        protected string User
        {
            get
            {
                if (!this.Request.Cookies.ContainsCookie(".auth-cakes"))
                {
                    return null;
                }

                var cookie = this.Request.Cookies.GetCookie(".auth-cakes");
                var cookieContent = cookie.Value;
                var username = this.UserCookieService.GetUserData(cookieContent);
                return username;
            }

        }

        protected IUserCookieService UserCookieService { get; }

        protected IHttpResponse View(string viewName, Dictionary<string, string> viewBag = null)
        {
            if (viewBag == null)
            {
                viewBag = new Dictionary<string, string>();
            }

            string allContent = this.GetAllContent(viewName, viewBag);

            this.PrepareHtmlResult(allContent);

            return this.Response;
        }

        private string GetAllContent(string viewName, IDictionary<string, string> viewBag)
        {
            string bodyContent = File.ReadAllText("Views/" + viewName + ".html");
            foreach (var item in viewBag)
            {
                bodyContent = bodyContent.Replace("@Model." + item.Key, item.Value);
            }
            string viewContent = File.ReadAllText("Views/_Layout.html").Replace("@BodyContent", bodyContent);

            return viewContent;
        }

        protected IHttpResponse BadRequestError(string errorMessage)
        {
            Dictionary<string, string> viewBag = new Dictionary<string, string>();
            viewBag.Add("Error", errorMessage);
            var allContent = this.GetAllContent("Error", viewBag);
            this.PrepareHtmlResult(allContent);
            this.Response.StatusCode = HttpResponseStatusCode.BadRequest;

            return this.Response;
        }

        protected IHttpResponse Redirect(string location)
        {
            this.Response.Headers.Add(new HttpHeader("Location", location));
            this.Response.StatusCode = HttpResponseStatusCode.SeeOther;
            return this.Response;
        }

        protected IHttpResponse Text(string content)
        {
            this.Response.Headers.Add(new HttpHeader("Content-Type", "text/plain"));
            this.Response.Content = Encoding.UTF8.GetBytes(content);
            return this.Response;
        }

        private void PrepareHtmlResult(string content)
        {
            this.Response.Headers.Add(new HttpHeader("Content-Type", "text/html charset=utf-8"));
            this.Response.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
