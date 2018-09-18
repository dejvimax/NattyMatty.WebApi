using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using NattyMatty.WebApi.Core;
using NattyMatty.WebApi.Models;
using NattyMatty.WebApi.ViewModels;

namespace NattyMatty.WebApi.InquiryProcessing
{
    public class AllProductsInquiryProcessor : IAllProductsInquiryProcessor
    {
        private readonly ProductContext _context;
        private readonly ILogger _logger;
        public AllProductsInquiryProcessor(ProductContext context, ILogger<AllProductsInquiryProcessor> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<ProductViewModel> GetProducts()
        {
            _logger.LogInformation(LoggingEvents.ListProducts, "Listing all products");

            var result = _context.Products.ToList();

            List<ProductViewModel> products = result.Select(x => new ProductViewModel()
            {
                //do your variable mapping here 
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return products;
        }
    }
}