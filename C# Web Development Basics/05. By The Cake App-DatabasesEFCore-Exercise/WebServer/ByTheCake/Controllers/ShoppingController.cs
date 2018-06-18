namespace HTTPServer.ByTheCake.Controllers
{
    using Models;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ShoppingController : Controller
    {
        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);
            Product cake = null;
            using (Context)
            {
                cake = Context.Products.Find(id);
            }

            if (cake == null)
            {
                return new NotFoundResponse();
            }

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            shoppingCart.Orders.Add(cake);

            var redirectUrl = "/search";

            const string searchTermKey = "searchTerm";

            if (req.UrlParameters.ContainsKey(searchTermKey))
            {
                redirectUrl = $"{redirectUrl}?{searchTermKey}={req.UrlParameters[searchTermKey]}";
            }

            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (!shoppingCart.Orders.Any())
            {
                this.ViewData["cartItems"] = "No items in your cart";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var items = shoppingCart
                    .Orders
                    .Select(i => $"<div>{i.Name} - ${i.Price:F2}</div><br />");

                var totalPrice = shoppingCart
                    .Orders
                    .Sum(i => i.Price);

                this.ViewData["cartItems"] = string.Join(string.Empty, items);
                this.ViewData["totalCost"] = $"{totalPrice:F2}";
            }

            return this.FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder(IHttpRequest req)
        {
            IEnumerable<int> productIds = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey).Orders
                .Select(o => o.Id);
            var currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);
            var order = new Order()
            {
                UserId = currentUserId,
                CreationDate = DateTime.UtcNow,
                Products = productIds
                .Select(id => new ProductOrder()
                {
                    ProductId = id,
                }).ToList()
            };
            using (Context)
            {
                Context.Orders.Add(order);
                Context.SaveChanges();
            }

            req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey).Orders.Clear();
            return this.FileViewResponse(@"shopping\finish-order");
        }

        public IHttpResponse ShowOrders(IHttpRequest req)
        {
            var sb = new StringBuilder();
            var currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);
            IEnumerable<Order> orders = new List<Order>();
            orders = Context.Orders.Where(u => u.UserId == currentUserId).OrderBy(o => o.CreationDate);
            foreach (Order order in orders)
            {
                decimal sum = Context.Orders.Where(u => u.UserId == currentUserId && u.Id == order.Id).Sum(x => x.Products.Sum(p => p.Product.Price));

                sb.AppendLine($"<tr><td><a href = \"/orderDetails/{order.Id}\">{order.Id}</a></td ><td>{order.CreationDate.ToString("dd-MM-yyyy")}</td><td>${sum:f2}</td></tr>");
            }
            this.ViewData["results"] = sb.ToString();
            return this.FileViewResponse(@"shopping\list-orders");
        }

        public IHttpResponse OrderDetails(int id)
        {
            var sb = new StringBuilder();
            var products = Context.ProductOrders.Where(o => o.OrderId == id);

            var order = Context.Orders.FirstOrDefault(o => o.Id == id);

            foreach (ProductOrder productOrder in products)
            {
                var product = Context.Products.FirstOrDefault(p => p.Id == productOrder.ProductId);
                sb.AppendLine($"<tr><td><a href = \"/cakeDetails/{product.Id}\">{product.Name}</a></td ><td>${product.Price:f2}</td></tr>");
            }
            this.ViewData["id"] = id.ToString();
            this.ViewData["results"] = sb.ToString();
            this.ViewData["date"] = order.CreationDate.ToString("dd-MM-yyyy");

            return this.FileViewResponse(@"shopping\order-details");
        }
    }
}