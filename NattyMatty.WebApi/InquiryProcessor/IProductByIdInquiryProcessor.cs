using NattyMatty.WebApi.ViewModels;

namespace NattyMatty.WebApi.InquiryProcessing
{
    public interface IProductByIdInquiryProcessor
    {
        ProductViewModel GetProduct(long productId);
    }
}