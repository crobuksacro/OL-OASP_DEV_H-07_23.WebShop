using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.CompanyModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.Common;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.CompanyModels
{
    public class CompanyUpdateBinding : CompanyBase
    {
        public long Id { get; set; }
        public AddressUpdateBinding? Address { get; set; }
    }
}
