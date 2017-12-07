namespace ProductsShop.App
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using ProductsShop.Data;
    using ProductsShop.Models;

    using Newtonsoft.Json;
    class StartUp
    {
        static void Main()
        {
            //RessetDatabase();
            //Console.WriteLine(ImportUserFromJson());
            //Console.WriteLine(ImportCategoriesFromJson());
            //Console.WriteLine(ImportProductsFromJson());
            //SetCategories();

            //GetProductsInRange();
            //GetSuccessfullySoldProducts();
            //GetCategoriesByProductsCount();
            //UsersAndProducts();

            //Console.WriteLine(importUsersFromXml());
            //Console.WriteLine(importCategoriesFromXml());
            //Console.WriteLine(importProductsFromXml());

            //GetProductsInRangeXml();
            //GetSoldProductsXml();
            //GetCategoriesByProductsCountXml();
            GetUsersAndProductsXm();

        }

        private static void GetUsersAndProductsXm()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(u => u.SoldProducts.Any())
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age ?? 0,
                        soldProducts = new
                        {
                            count = u.SoldProducts.Count,
                            products = u.SoldProducts.Select(p => new
                            {
                                name = p.Name,
                                price = p.Price
                            })
                        }
                    }).OrderByDescending(u => u.soldProducts.count)
                    .ThenBy(u => u.lastName)
                    .ToArray();
                var jsonReady = new
                {
                    usersCount = users.Length,
                    users = users
                };
                var xDoc = new XDocument();
                xDoc.Add(new XElement("users", new XAttribute("count", jsonReady.usersCount)));
                foreach (var u in jsonReady.users)
                {
                    var element = new XElement("user");

                    if (u.firstName != null)
                    {
                        element.Add(new XAttribute("first-name", u.firstName));
                    }
                    element.Add(new XAttribute("last-name", u.lastName));
                    if (u.age != null)
                    {
                        element.Add(new XAttribute("age", u.age));
                    }
                    var productsElement = new XElement("sold-products", new XAttribute("count", u.soldProducts.count));
                    foreach (var e in u.soldProducts.products)
                    {
                        productsElement.Add(new XElement("product",
                            new XAttribute("name", e.name),
                            new XAttribute("price", e.price)));
                    }
                    element.Add(productsElement);
                    xDoc.Root.Add(element);

                }
                xDoc.Save("UsersAndProducts.xml");
            }
        }
        static void GetCategoriesByProductsCountXml()
        {
            using (var db = new ProductsShopContext())
            {
                var categories = db.Categories.OrderByDescending(x => x.Products.Count).Select(x => new
                {
                    x.Name,
                    productsCount = x.Products.Count,
                    averagePrice = x.Products.Select(y => y.Product.Price).Average(),
                    totalRevenue = x.Products.Select(y => y.Product.Price).Sum()
                });
                var xDoc = new XDocument();
                xDoc.Add(new XElement("categories"));
                foreach (var c in categories)
                {
                    xDoc.Root.Add(new XElement("category",
                        new XAttribute("name", c.Name),
                        new XElement("products-count", c.productsCount),
                        new XElement("average-price", c.averagePrice),
                        new XElement("total-revenue", c.totalRevenue)));
                }
                xDoc.Save("CategoriesByProductsCountXml.xml");
            }
        }
        static void GetSoldProductsXml()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(x => x.SoldProducts.Any(p => p.BuyerId != null))
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .Select(p => new
                    {
                        p.Id,
                        p.FirstName,
                        p.LastName,
                        soldProducts = p.SoldProducts
                            .Select(x => new
                            {
                                x.SellerId,
                                x.Name,
                                x.Price,
                            })
                    });
                var xDoc = new XDocument(new XElement("users"));
                foreach (var u in users)
                {
                    var element = new XElement("user");

                    if (u.FirstName != null)
                    {
                        element.Add(new XAttribute("first-name", u.FirstName));
                    }

                    element.Add(new XAttribute("last-name", u.LastName));

                    var productsElement = new XElement("sold-products");
                    foreach (var product in u.soldProducts)
                    {
                        productsElement.Add(new XElement("product",
                            new XElement("name", product.Name),
                            new XElement("price", product.Price)));
                    }

                    element.Add(productsElement);
                    xDoc.Root.Add(element);
                }
                xDoc.Save("SoldProducts.xml");
            }
        }

        private static void GetProductsInRangeXml()
        {
            using (var db = new ProductsShopContext())
            {
                var products = db.Products
                    .Where(p => p.Price >= 1000 && p.Price <= 2000 && p.BuyerId != null)
                    .Select(x => new
                    {
                        x.Name,
                        x.Price,
                        buyer = $"{x.Buyer.FirstName} {x.Buyer.LastName}"
                    }).ToArray();

                //    Console.WriteLine(products.Length);

                var xDoc = new XDocument(new XElement("products"));
                foreach (var p in products)
                {
                    xDoc.Root.Add(new XElement("product",
                        new XAttribute("name", p.Name),
                        new XAttribute("price", p.Price),
                        new XAttribute("buyer", p.buyer)));
                }
                xDoc.Save("ProductsInRange.xml");
            }
        }

        static string importProductsFromXml()
        {
            var path = "Files/products.xml";
            var xmlString = File.ReadAllText(path);

            var xmlDoc = XDocument.Parse(xmlString);
            var elements = xmlDoc.Root.Elements();
            var catProducts = new List<CategoryProduct>();
            using (var db = new ProductsShopContext())
            {

                var usersIds = db.Users.Select(u => u.Id).ToArray();
                var categoryIds = db.Categories.Select(c => c.Id).ToArray();

                var rnd = new Random();
                foreach (var e in elements)
                {
                    string name = e.Element("name").Value;
                    decimal price = decimal.Parse(e.Element("price").Value);

                    var sellerIndex = rnd.Next(0, usersIds.Length);

                    int sellerId = usersIds[sellerIndex];

                    var product = new Product()
                    {
                        Name = name,
                        Price = price,
                        SellerId = sellerId
                    };
                    int categoryIndex = rnd.Next(0, categoryIds.Length);
                    int categoryId = categoryIds[categoryIndex];

                    var catProduct = new CategoryProduct()
                    {
                        Product = product,
                        CategoryId = categoryId
                    };
                    catProducts.Add(catProduct);
                }
                db.CategoriesProducts.AddRange(catProducts);
                db.SaveChanges();
            }
            return $"{catProducts.Count} elements were imported from file: {path}";
        }

        static string importCategoriesFromXml()
        {
            var path = "Files/categories.xml";
            var xmlString = File.ReadAllText(path);

            var xmlDoc = XDocument.Parse(xmlString);
            var elements = xmlDoc.Root.Elements();
            var categories = new List<Category>();

            foreach (var e in elements)
            {
                var nameString = e.Element("name").Value;
                var category = new Category()
                {
                    Name = nameString
                };
                categories.Add(category);
            }

            using (var db = new ProductsShopContext())
            {
                db.Categories.AddRange(categories);
                db.SaveChanges();
            }
            return $"{categories.Count} categories were imported from file: {path}";
        }

        static string importUsersFromXml()
        {
            var path = "Files/users.xml";
            var xmlString = File.ReadAllText(path);

            var xmlDoc = XDocument.Parse(xmlString);
            var elements = xmlDoc.Root.Elements();
            var users = new List<User>();
            foreach (var e in elements)
            {
                var firstName = e.Attribute("firstName")?.Value;
                var lastName = e.Attribute("lastName").Value;

                int? age = null;
                if (e.Attribute("age") != null)
                {
                    age = int.Parse(e.Attribute("age").Value);
                }
                var user = new User(lastName)
                {
                    FirstName = firstName,
                    Age = age
                };
                users.Add(user);
            }
            using (var db = new ProductsShopContext())
            {
                db.Users.AddRange(users);
                db.SaveChanges();
            }
            return $"{users.Count} users were imported from file: {path}";
        }

        static void UsersAndProducts()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(u => u.SoldProducts.Any())
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age ?? 0,
                        soldProducts = new
                        {
                            count = u.SoldProducts.Count,
                            products = u.SoldProducts.Select(p => new
                            {
                                name = p.Name,
                                price = p.Price
                            })
                        }
                    })
                    .OrderByDescending(u => u.soldProducts.count)
                    .ThenBy(u => u.lastName)
                    .ToArray();
                var jsonReady = new
                {
                    usersCount = users.Length,
                    users = users
                };

                var jsonString = JsonConvert.SerializeObject(jsonReady, Formatting.Indented, new JsonSerializerSettings()
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

                File.WriteAllText("UsersAndProducts", jsonString);
            }
        }

        static void GetCategoriesByProductsCount()
        {
            using (var db = new ProductsShopContext())
            {
                var categories = db.Categories.OrderBy(x => x.Name).Select(x => new
                {
                    x.Name,
                    productsCount = x.Products.Count,
                    averagePrice = x.Products.Select(y => y.Product.Price).Average(),
                    totalRevenue = x.Products.Select(y => y.Product.Price).Sum()
                });
                var jsonString = JsonConvert.SerializeObject(categories, Formatting.Indented, new JsonSerializerSettings()
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

                File.WriteAllText("CategoriesByProductsCount", jsonString);
            }
        }

        static void GetSuccessfullySoldProducts()
        {
            using (var db = new ProductsShopContext())
            {
                var users = db.Users
                    .Where(x => x.SoldProducts.Any(p => p.BuyerId != null))
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .Select(p => new
                    {
                        p.FirstName,
                        p.LastName,
                        soldProducts = p.SoldProducts
                            .Select(x => new
                            {
                                x.Name,
                                x.Price,
                                x.Buyer.FirstName,
                                x.Buyer.LastName
                            })
                    });
                var jsonString = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings()
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

                File.WriteAllText("SoldProducts", jsonString);
            }
        }

        static void GetProductsInRange()
        {
            using (var db = new ProductsShopContext())
            {
                var products = db.Products.Where(x => x.Price >= 500 && x.Price <= 1000).OrderBy(p => p.Price).Select(x => new
                {
                    x.Name,
                    x.Price,
                    Seller = $"{x.Seller.FirstName} {x.Seller.LastName}"
                });
                var jsonString = JsonConvert.SerializeObject(products, Formatting.Indented);
                File.WriteAllText("PriceInRange", jsonString);
            }
        }

        static void SetCategories()
        {
            using (var db = new ProductsShopContext())
            {
                var productIds = db.Products.Select(p => p.Id).ToArray();
                var categoryIds = db.Categories.Select(p => p.Id).ToArray();

                var rnd = new Random();

                var categoryProducts = new List<CategoryProduct>();

                foreach (var p in productIds)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var categoryIndex = rnd.Next(0, categoryIds.Length);

                        while (categoryProducts.Any(cp => cp.ProductId == p && cp.CategoryId == categoryIds[categoryIndex]))
                        {
                            categoryIndex = rnd.Next(0, categoryIds.Length);
                        }


                        var catPr = new CategoryProduct()
                        {
                            ProductId = p,
                            CategoryId = categoryIds[categoryIndex]
                        };
                        categoryProducts.Add(catPr);

                    }
                }
                db.CategoriesProducts.AddRange(categoryProducts);
                db.SaveChanges();
            }
        }

        static string ImportProductsFromJson()
        {
            var path = "Files/products.json";
            Random rnd = new Random();
            Product[] products = ImportJson<Product>(path);

            using (var db = new ProductsShopContext())
            {
                int[] userIds = db.Users.Select(u => u.Id).ToArray();
                foreach (var p in products)
                {
                    int sellerId = userIds[rnd.Next(0, userIds.Length)];
                    int? buyerId = sellerId;
                    while (buyerId == sellerId)
                    {
                        int buyerIndex = rnd.Next(0, userIds.Length);
                        buyerId = userIds[buyerIndex];
                    }
                    if (buyerId - sellerId < 5 && buyerId - sellerId > 0)
                    {
                        buyerId = null;
                    }
                    p.SellerId = sellerId;
                    p.BuyerId = buyerId;
                }
                db.Products.AddRange(products);

                db.SaveChanges();
            }
            return $"{products.Length} products were imported from: {path}";
        }
        static string ImportCategoriesFromJson()
        {
            var path = "Files/categories.json";
            Category[] categories = ImportJson<Category>(path);
            using (var db = new ProductsShopContext())
            {
                db.Categories.AddRange(categories);
                db.SaveChanges();
            }
            return $"{categories.Length} categories were imported from: {path}";
        }
        static string ImportUserFromJson()
        {
            var path = "Files/users.json";
            User[] users = ImportJson<User>(path);
            using (var db = new ProductsShopContext())
            {
                db.Users.AddRange(users);
                db.SaveChanges();
            }
            return $"{users.Length} users were imported from: {path}";
        }

        static T[] ImportJson<T>(string path)
        {
            string jsonString = File.ReadAllText(path);
            T[] objects = JsonConvert.DeserializeObject<T[]>(jsonString);

            return objects;
        }

        static void RessetDatabase()
        {
            using (var db = new ProductsShopContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
