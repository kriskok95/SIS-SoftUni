using SIS.HTTP.Enums;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using SIS.HTTP.Requests;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;

namespace CakeApp.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            return this.View("Index");
        }
         
        public IHttpResponse HelloUser()
        {
            return this.View("HelloUser", new Dictionary<string, string>()
            {
                {"Username", this.User }
            });
        }
    }
}
