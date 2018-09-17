using Newtonsoft.Json;

namespace NattyMatty.WebApi.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ProductViewModel
    {
        public ProductViewModel()
        {            
        }

        public long Id { get; set; }

        public string Name { get; set; }
    }
}
