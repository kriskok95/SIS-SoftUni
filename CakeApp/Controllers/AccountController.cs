using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using CakeApp.Data;
using CakeApp.Models;
using SIS.HTTP.Cookies;
using SIS.HTTP.Requests;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;
using SIS.MvcFramework.Services;

namespace CakeApp.Controllers
{
    public class AccountController : BaseController
    {
        private readonly HashService hashService;

        public AccountController()
        {
            this.hashService = new HashService();
        }

        public IHttpResponse Register()
        {
            return this.View("Register");
        }

        public IHttpResponse DoRegister()
        {
            string username = this.Request.FormData["username"].ToString();
            string password = this.Request.FormData["password"].ToString();
            string confirmPassword = this.Request.FormData["confirmPassword"].ToString();

            string hashedPassword = this.hashService.Hash(password);
            string hashedConfirmPassword = this.hashService.Hash(confirmPassword);

            if (string.IsNullOrWhiteSpace(username) || username.Length < 5)
            {
                return this.BadRequestError("Please provide valid username with length at least 4 characters!");
            }

            if (this.Db.Users.Any(x => x.Username == username))
            {
                return this.BadRequestError($"Username: {username} already exists in the database");
            }

            if (hashedPassword != hashedConfirmPassword)
            {
                return this.BadRequestError("The password does not match!");
            }

            User user = new User()
            {
                Name = username,
                Username = username,
                Password = hashedPassword,
                DateOfRegistration = DateTime.UtcNow
            };

            this.Db.Users.Add(user);
            this.Db.SaveChanges();

            return this.View("Index");
        }

        public IHttpResponse Login()
        {
            return this.View("Login");
        }

        public IHttpResponse DoLogin()
        {
            string username = this.Request.FormData["username"].ToString().Trim();
            string password = this.Request.FormData["password"].ToString();

            string hashedPassword = this.hashService.Hash(password);

            var user = this.Db.Users.FirstOrDefault(x => x.Username == username && x.Password == hashedPassword);

            if (user == null)
            {
                return this.BadRequestError("Invalid username or password");
            }

            var cookieContent = this.UserCookieService.GetUserCookie(user.Username);

            this.Response.Cookies.Add(new HttpCookie(".auth-cakes", cookieContent, 7));

            return this.Redirect("/");

        }

        public IHttpResponse LogOut()
        {
            if (!this.Request.Cookies.ContainsCookie(".auth-cakes"))
            {
                return this.Redirect("/");
            }

            var cookie = this.Request.Cookies.GetCookie(".auth-cakes");
            cookie.Delete();
            this.Response.Cookies.Add(cookie);

            return this.Redirect("/");
        }
    }
}
