using Xunit;
using NattyMatty.WebApi.Controllers;
using NattyMatty.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace NattyMatty.WebApi.Test
{
    public class ProductController_GetAllShould
    {    

        public ProductController_GetAllShould()
        {            
        }

        [Fact]
        public void ReturnListOfProducts()
        {
            using (var context = GetContextWithData())
            using (var controller = new ProductController(context))
            {
                var result = controller.GetAll();

                Assert.NotNull(result);
                Assert.Equal(12, result.ToList().Count());
            }
        }  

        private ProductContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductContext>().UseInMemoryDatabase("ProductList").Options;
            
            var context = new ProductContext(options);            

            context.Products.Add(new Product { Id = 1, Name = "La Trappe Isid'or" });
            context.Products.Add(new Product { Id = 2, Name = "St. Bernardus Abt 12" });
            context.Products.Add(new Product { Id = 3, Name = "Zundert" });
            context.Products.Add(new Product { Id = 4, Name = "La Trappe Blond" });
            context.Products.Add(new Product { Id = 5, Name = "La Trappe Bock" });
            context.Products.Add(new Product { Id = 6, Name = "St. Bernardus Tripel" });
            context.Products.Add(new Product { Id = 7, Name = "Grottenbier Bruin" });
            context.Products.Add(new Product { Id = 8, Name = "St. Bernardus Pater 6" });
            context.Products.Add(new Product { Id = 9, Name = "La Trappe Quadrupel" });
            context.Products.Add(new Product { Id = 10, Name = "Westvleteren 12" });
            context.Products.Add(new Product { Id = 11, Name = "Leffe Bruin" });
            context.Products.Add(new Product { Id = 12, Name = "Leffe Royale" });
            context.SaveChanges();

            return context;
        }
    }
}