using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NattyMatty.WebApi.Core;
using NattyMatty.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

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
            _logger.LogError(LoggingEvents.ListProducts, "Error: Listing all products");

            _logger.LogTrace("Hello world : Trace");
            _logger.LogDebug("Hello world : Debug");
            _logger.LogInformation("Hello world : Information");
            _logger.LogError("Hello world : Error");
            _logger.LogCritical("Hello world : Critical");
            _logger.LogWarning("Hello world : Warning");

            var result = _context.Products.ToList();

            return new JsonResult(result
                , new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        /*public IEnumerable<Product> GetAll()
        {
            _logger.LogInformation(LoggingEvents.ListProducts, "Listing all products");
            _logger.LogError(LoggingEvents.ListProducts, "Error: Listing all products");

            _logger.LogTrace("Hello world : Trace");
            _logger.LogDebug("Hello world : Debug");
            _logger.LogInformation("Hello world : Information");
            _logger.LogError("Hello world : Error");
            _logger.LogCritical("Hello world : Critical");
            _logger.LogWarning("Hello world : Warning");

            return _context.Products.ToList();
        }*/

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetById(long id)
        {
            var product = _context.Products.FirstOrDefault(t => t.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return new JsonResult(product
                , new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
            //return new ObjectResult(product);
        }

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
        }
    }
}


