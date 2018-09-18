
using System.Collections.Generic;
using NattyMatty.WebApi.ViewModels;

namespace NattyMatty.WebApi.InquiryProcessing
{
    public interface IAllProductsInquiryProcessor
    {
        List<ProductViewModel> GetProducts();
    }
}