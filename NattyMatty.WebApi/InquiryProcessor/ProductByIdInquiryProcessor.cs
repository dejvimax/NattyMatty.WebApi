using NattyMatty.WebApi.Models;
using Microsoft.Extensions.Logging;
using NattyMatty.WebApi.Core;
using System.Linq;
using NattyMatty.WebApi.Data.Exceptions;
using NattyMatty.WebApi.ViewModels;

namespace NattyMatty.WebApi.InquiryProcessing
{
    public class ProductByIdInquiryProcessor : IProductByIdInquiryProcessor
    {
        private readonly ProductContext _context;
        private readonly ILogger _logger;

        public ProductByIdInquiryProcessor(ProductContext context, ILogger<ProductByIdInquiryProcessor> logger)
        {
            _context = context;
            _logger = logger;
        }

        public ProductViewModel GetProduct(long productId)
        {
            _logger.LogInformation(LoggingEvents.GetProduct, $"Get product: '{productId}'");
            var product = _context.Products.FirstOrDefault(t => t.Id == productId);

            if (product == null)
            {
                throw new RootObjectNotFoundException("Product not found");
            }

            _logger.LogInformation(LoggingEvents.GetProduct, $"Product '{product.Name}' found for Id: '{productId}'");

            var productViewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name
            };

            return productViewModel;

        }
    }
}