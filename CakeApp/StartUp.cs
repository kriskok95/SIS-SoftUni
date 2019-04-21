using CakeApp.Controllers;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Routing;
using System;
using SIS.MvcFramework.Contracts;

namespace CakeApp
{
    public class StartUp : IMvcApplication

    {
        public void Configure(ServerRoutingTable routing)
        {
            routing.Routes[HttpRequestMethod.Get]["/"] = request => new HomeController { Request = request }.Index();
            routing.Routes[HttpRequestMethod.Get]["/register"] = request => new AccountController { Request = request }.Register();
            routing.Routes[HttpRequestMethod.Post]["/register"] = request => new AccountController { Request = request }.DoRegister();
            routing.Routes[HttpRequestMethod.Get]["/login"] = request => new AccountController { Request = request }.Login();
            routing.Routes[HttpRequestMethod.Post]["/login"] = request => new AccountController { Request = request }.DoLogin();
            routing.Routes[HttpRequestMethod.Get]["/hello"] = request => new HomeController { Request = request }.HelloUser();
            routing.Routes[HttpRequestMethod.Get]["/logout"] = request => new AccountController { Request = request }.LogOut();
            routing.Routes[HttpRequestMethod.Get]["/cake/add"] = request => new CakeController { Request = request }.AddCake();
            routing.Routes[HttpRequestMethod.Post]["/cake/add"] = request => new CakeController { Request = request }.DoAddCake();
            routing.Routes[HttpRequestMethod.Get]["/cake/view"] = request => new CakeController { Request = request }.ViewCake();
        }

        public void ConfigureServices()
        {
            // TODO: Implement IoC/DI container
        }
    }
}
