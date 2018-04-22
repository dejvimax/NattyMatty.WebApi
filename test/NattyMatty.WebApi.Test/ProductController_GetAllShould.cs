using Xunit;
using NattyMatty.WebApi.Controllers;

using NattyMatty.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace NattyMatty.WebApi.Test
{
    public class ProductController_GetAllShould
    {
        //private readonly ProductController _productController;

        public ProductController_GetAllShould()
        {
            //_productController = new ProductController();
        }

        [Fact]
        public void ReturnListOfProducts()
        {
            using (var context = GetContextWithData())
            using (var controller = new ProductController(context))
            {
                var result = controller.GetAll();

                Assert.NotNull(result);
                //Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
            }

        }

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }

        private ProductContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductContext>().UseInMemoryDatabase("ProductList").Options;

            //var options = new DbContextOptionsBuilder<MyDbContext>()
            //                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
            //                  .Options;
            var context = new ProductContext(options);

            //var beerCategory = new Category { Id = 1, Name = "Beers" };
            //var wineCategory = new Category { Id = 2, Name = "Wines" };
            //context.Categories.Add(beerCategory);
            //context.Categories.Add(wineCategory);

            context.Products.Add(new Product { Id = 1, Name = "La Trappe Isid'or" });
            context.Products.Add(new Product { Id = 2, Name = "St. Bernardus Abt 12" });
            context.Products.Add(new Product { Id = 3, Name = "Zundert"});
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


            /*var options = new DbContextOptionsBuilder<MyDbContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new MyDbContext(options);

            var beerCategory = new Category { Id = 1, Name = "Beers" };
            var wineCategory = new Category { Id = 2, Name = "Wines" };
            context.Categories.Add(beerCategory);
            context.Categories.Add(wineCategory);

            context.Products.Add(new Product { Id = 1, Name = "La Trappe Isid'or", Category = beerCategory });
            context.Products.Add(new Product { Id = 2, Name = "St. Bernardus Abt 12", Category = beerCategory });
            context.Products.Add(new Product { Id = 3, Name = "Zundert", Category = beerCategory });
            context.Products.Add(new Product { Id = 4, Name = "La Trappe Blond", Category = beerCategory });
            context.Products.Add(new Product { Id = 5, Name = "La Trappe Bock", Category = beerCategory });
            context.Products.Add(new Product { Id = 6, Name = "St. Bernardus Tripel", Category = beerCategory });
            context.Products.Add(new Product { Id = 7, Name = "Grottenbier Bruin", Category = beerCategory });
            context.Products.Add(new Product { Id = 8, Name = "St. Bernardus Pater 6", Category = beerCategory });
            context.Products.Add(new Product { Id = 9, Name = "La Trappe Quadrupel", Category = beerCategory });
            context.Products.Add(new Product { Id = 10, Name = "Westvleteren 12", Category = beerCategory });
            context.Products.Add(new Product { Id = 11, Name = "Leffe Bruin", Category = beerCategory });
            context.Products.Add(new Product { Id = 12, Name = "Leffe Royale", Category = beerCategory });
            context.SaveChanges();*/

            return context;
        }
    }
}

/*
using Xunit;
using Prime.Services;

namespace Prime.UnitTests.Services
{
    public class PrimeService_IsPrimeShould
    {
        private readonly PrimeService _primeService;

        public PrimeService_IsPrimeShould()
        {
            _primeService = new PrimeService();
        }

        [Fact]
        public void ReturnFalseGivenValueOf1()
        {
            var result = _primeService.IsPrime(1);

            Assert.False(result, "1 should not be prime");
        }
    }
}
*/
