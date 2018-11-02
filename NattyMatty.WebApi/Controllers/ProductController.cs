using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NattyMatty.WebApi.Core;
using NattyMatty.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System;
using Mapster;
using NattyMatty.WebApi.ViewModels;

namespace NattyMatty.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductContext _context;
        private readonly ILogger _logger;

        public ProductController(ProductContext context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;

            /*
            if (_context.Products.Count() == 0)
            {
                _context.Products.Add(new Product { Name = "Sleeping Beauty" });
                _context.SaveChanges();
            }*/
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation(LoggingEvents.ListProducts, "Listing all products");
            
            /*_logger.LogTrace("Hello world : Trace");
            _logger.LogDebug("Hello world : Debug");
            _logger.LogInformation("Hello world : Information");            
            _logger.LogCritical("Hello world : Critical");
            _logger.LogWarning("Hello world : Warning");*/

            var result = _context.Products.ToList();

            // return new JsonResult(result
            //     , new JsonSerializerSettings()
            //     {
            //         Formatting = Formatting.Indented
            //     });


            return new JsonResult(result.Adapt<List<ProductViewModel>>()
                , new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }        

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(long id)
        {
            _logger.LogInformation(LoggingEvents.GetProduct, $"Get product: '{id}'");
            var product = _context.Products.FirstOrDefault(t => t.Id == id);

            if (product == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Product ID {0} has not been found", id)
                });
            }

            _logger.LogInformation(LoggingEvents.GetProduct, $"Product '{product.Name}' found for Id: '{id}'");
            
            return new JsonResult(product.Adapt<ProductViewModel>()
                , new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// Adds a new Product to the Database
        /// </summary>
        /// <param name="model">The ProductViewModel containing the data to insert</param>
        [HttpPut]
        public IActionResult Put([FromBody]ProductViewModel model)
        {
            // return a generic HTTP Status 500 (Server Error)
            // if the client payload is invalid.
            if (model == null) return new StatusCodeResult(500);

            // map the ViewModel to the Model
            var product = model.Adapt<Product>();            

            _context.Products.Add(product);
            _context.SaveChanges();            

            // return the newly-created Product to the client.
            return new JsonResult(product.Adapt<ProductViewModel>()
                , new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// Edit the Product with the given {id}
        /// </summary>
        /// <param name="model">The ProductViewModel containing the data to update</param>
        [HttpPost]
        public IActionResult Post([FromBody]ProductViewModel model)
        {
            // return a generic HTTP Status 500 (Server Error)
            // if the client payload is invalid.
            if (model == null) return new StatusCodeResult(500);

            // retrieve the product to edit
            var product = _context.Products.Where(p => p.Id ==
                        model.Id).FirstOrDefault();

            // handle requests asking for non-existing quizzes
            if (product == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Product ID {0} has not been found", model.Id)
                });
            }

            // handle the update (without object-mapping)
            //   by manually assigning the properties 
            //   we want to accept from the request
            product.Name = model.Name;            

            // persist the changes into the Database.
            _context.SaveChanges();

            // return the updated Quiz to the client.
            return new JsonResult(product.Adapt<ProductViewModel>()
                , new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// Deletes the Product with the given {id} from the Database
        /// </summary>
        /// <param name="id">The ID of an existing Product</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // retrieve the quiz from the Database
            var product = _context.Products.Where(i => i.Id == id)
                .FirstOrDefault();

            // handle requests asking for non-existing products
            if (product == null)
            {
                return NotFound(new
                {
                    Error = String.Format("Product ID {0} has not been found", id)
                });
            }

            // remove the product from the DbContext.
            _context.Products.Remove(product);
            // persist the changes into the Database.
            _context.SaveChanges();

            // return an HTTP Status 200 (OK).
            // return new OkResult();

            // [2018.01.26] BOOK ERRATA: return a NoContentResult to comply with a bug in the Angular 5 HttpRouter (fixed on December 2017, see here: https://github.com/angular/angular/issues/19502) which expects a JSON by default 
            // and will throw a SyntaxError: Unexpected end of JSON input in case of an HTTP 200 result with no content.
            // ref.: https://github.com/angular/angular/issues/19502
            // ref.: https://github.com/PacktPublishing/ASP.NET-Core-2-and-Angular-5/issues/19
            return new NoContentResult();
        }

        /*

        /// <summary>
        /// Creates a Product.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Product
        ///     {
        ///        "id": 1,
        ///        "name": "Product1"
        ///     }
        ///
        /// </remarks>
        /// <param name="product"></param>
        /// <returns>A newly created Product</returns>
        /// <response code="201">Returns the newly created product</response>
        /// <response code="400">If the product is null</response>            
        [HttpPost]
        [ProducesResponseType(typeof(Product), 201)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Product product)
        {
            if (product == null || product.Id != id)
            {
                return BadRequest();
            }

            var productToUpdate = _context.Products.FirstOrDefault(t => t.Id == id);
            if (productToUpdate == null)
            {
                return NotFound();
            }

            productToUpdate.Name = product.Name;

            _context.Products.Update(productToUpdate);
            _context.SaveChanges();
            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific Product.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Products.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Products.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }*/ 
    }
}


