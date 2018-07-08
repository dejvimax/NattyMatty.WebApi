using NattyMatty.WebApi.Models;
using System;
using System.Linq;

namespace NattyMatty.WebApi.Data
{
    public class DbSeeder
    {
        public static void Seed(ProductContext dbContext)
        {
            if (!dbContext.Products.Any())
            {
                CreateProducts(dbContext);
            }
        }

        private static void CreateProducts(ProductContext dbContext)
        {
#if DEBUG
            // create 10 sample products with auto-generated data            
            var num = 10;
            for (int i = 1; i <= num; i++)
            {
                var product = new Product()
                {
                    Name = String.Format("Prouct {0} Name", i)
                };
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }
#endif
        }
    }
}
