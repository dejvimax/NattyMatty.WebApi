namespace NattyMatty.WebApi.Core
{
    public class LoggingEvents
    {
        public const int GenerateProducts = 1000;
        public const int ListProducts = 1001;
        public const int GetProduct = 1002;
        public const int InsertProduct = 1003;
        public const int UpdateProduct = 1004;
        public const int DeleteProduct = 1005;

        public const int GetProductNotFound = 4000;
        public const int UpdateProductNotFound = 4001;
    }
}
