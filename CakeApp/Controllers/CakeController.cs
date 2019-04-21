using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CakeApp.Extensions;
using CakeApp.Models;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace CakeApp.Controllers
{
    public class CakeController : BaseController
    {
        public IHttpResponse AddCake()
        {
            return this.View("AddCake");
        }

        public IHttpResponse DoAddCake()
        {
            string name = this.Request.FormData["name"].ToString().Trim();
            decimal price = decimal.Parse(this.Request.FormData["price"].ToString());
            string url = this.Request.FormData["url"].ToString().UrlDecode();

            //TODO: Add validation

            Product product = new Product()
            {
                Name = name,
                Price = price,
                ImageURL = url
            };

            this.Db.Products.Add(product);

            try
            {
                this.Db.SaveChanges();
            }
            catch (Exception e)
            {
                this.BadRequestError(e.Message);
            }

            return this.Redirect("/");
        }

        public IHttpResponse ViewCake()
        {
            int cakeId = int.Parse(this.Request.QueryData["id"].ToString());
            Product cake = this.Db.Products
                .FirstOrDefault(x => x.Id == cakeId);

            if (cake == null)
            {
                throw new BadRequestException();
            }

            Dictionary<string, string> viewBag = new Dictionary<string, string>();
            viewBag.Add("Name", cake.Name);
            viewBag.Add("Price", cake.Price.ToString(CultureInfo.InvariantCulture));
            viewBag.Add("ImageUrl", cake.ImageURL);

            return this.View("ViewCake", viewBag);
        }
    }
}
