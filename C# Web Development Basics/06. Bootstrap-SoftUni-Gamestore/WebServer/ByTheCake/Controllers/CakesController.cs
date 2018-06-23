namespace HTTPServer.ByTheCake.Controllers
{
    using Infrastructure;
    using Models;
    using Server.Http.Response;
    using Server.Http.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CakesController : Controller
    {
        public IHttpResponse Add()
        {
            this.ViewData["showResult"] = "none";

            return this.FileViewResponse(@"cakes\add");
        }

        public IHttpResponse Add(string name, string price, string imageUrl)
        {
            var cake = new Product()
            {
                Name = name,
                Price = decimal.Parse(price),
                ImageUrl = imageUrl
            };
            using (this.Context)
            {
                this.Context.Products.Add(cake);
                this.Context.SaveChanges();
            }
            this.ViewData["name"] = name;
            this.ViewData["price"] = price;
            this.ViewData["image"] = imageUrl;
            this.ViewData["showResult"] = "block";

            return this.FileViewResponse(@"cakes\add");
        }

        public IHttpResponse Details(int id)
        {
            Product cake = null;
            using (this.Context)
            {
                cake = this.Context.Products.Find(id);
            }
            if (cake == null)
            {
                return new BadRequestResponse();
            }

            this.ViewData["name"] = cake.Name;
            this.ViewData["price"] = cake.Price.ToString("F2");
            this.ViewData["imageUrl"] = cake.ImageUrl;

            return this.FileViewResponse(@"cakes\details");
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            const string searchTermKey = "searchTerm";

            var urlParameters = req.UrlParameters;

            this.ViewData["results"] = string.Empty;
            this.ViewData["searchTerm"] = string.Empty;

            if (urlParameters.ContainsKey(searchTermKey))
            {
                var searchTerm = urlParameters[searchTermKey];

                this.ViewData["searchTerm"] = searchTerm;

                IEnumerable<string> cakeResult;
                using (this.Context)
                {
                    cakeResult = this.Context.Products.AsQueryable().Where(cake => cake.Name.ToLower().Contains(searchTerm.ToLower()))
                       .Select(c => $@"<div><a href=""/cakeDetails/{c.Id}"">{c.Name}</a> - ${c.Price:F2} <a href=""/shopping/add/{c.Id}?searchTerm={searchTerm}"">Order</a></div>")
                       .ToList();
                }

                var results = "No cakes found";

                if (cakeResult.Any())
                {
                    results = string.Join(Environment.NewLine, cakeResult);
                }

                this.ViewData["results"] = results;
            }
            else
            {
                this.ViewData["results"] = "Please, enter search term";
            }

            this.ViewData["showCart"] = "none";

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart.Orders.Any())
            {
                var totalProducts = shoppingCart.Orders.Count;
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";
            }

            return this.FileViewResponse(@"cakes\search");
        }
    }
}