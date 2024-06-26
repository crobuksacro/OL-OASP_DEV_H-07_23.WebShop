using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.CompanyModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.Common;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.CompanyModels
{
    public class CompanyViewModel : CompanyBase
    {
        public long Id { get; set; }
        public AddressViewModel? Address { get; set; }
        public long? AddressId { get; set; }
    }
}
