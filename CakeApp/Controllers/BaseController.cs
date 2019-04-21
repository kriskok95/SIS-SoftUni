using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using CakeApp.Data;
using SIS.HTTP.Enums;
using SIS.HTTP.Requests;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;
using SIS.MvcFramework;
using SIS.MvcFramework.Services;
using SIS.MvcFramework.Services.Contracts;

namespace CakeApp.Controllers
{

    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            this.Db = new CakeDbContext();
        }

        protected CakeDbContext Db { get; set; }
    }
}
