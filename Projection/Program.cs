using System;
using System.Linq;

namespace Projection
{
    class Program
    {
        static void Main()
        {
            using (var db = new NorthwindContext())
            {

                //var query = db.Products
                //    .Where(product => product.UnitPrice < 10M)
                //    .OrderByDescending(product => product.UnitPrice)
                //    .Select(product => new
                //    {
                //        product.ProductID,
                //        product.ProductName,
                //        product.UnitPrice
                //    });

                //Console.WriteLine("Products that cost less than $10.");
                //foreach (var item in query)
                //{
                //    Console.WriteLine($"{item.ProductID} : {item.ProductName} costs {item.UnitPrice:$#,##0.00}");
                //}

                var categories = db.Categories.Select(c => new
                {
                    c.CategoryID,
                    c.CategoryName
                }).ToArray();

                var products = db.Products.Select(p => new
                {
                    p.ProductID,
                    p.ProductName,
                    p.CategoryID
                }).ToArray();

                // join every product to its category to return 77 matches
                var queryJoin = categories.Join(products,
                    category => category.CategoryID,
                    product => product.CategoryID,
                    (c, p) => new { c.CategoryName, p.ProductName, p.ProductID })
                    .OrderBy(cp => cp.ProductID);

                foreach (var item in queryJoin)
                {
                    Console.WriteLine($"{item.ProductID} : {item.ProductName} is in {item.CategoryName}.");
                }

                // group all products by their category to return 8 matches
                var queryGroup = categories.GroupJoin(products,
                    category => category.CategoryID,
                    product => product.CategoryID,
                    (c, x) => new { c.CategoryName, Products = x.OrderBy(p => p.ProductName) });
                foreach (var item in queryGroup)
                {
                    Console.WriteLine($"{ item.CategoryName} has {item.Products.Count()} products.");
                    foreach (var product in item.Products)
                    {
                        Console.WriteLine($" {product.ProductName}");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
