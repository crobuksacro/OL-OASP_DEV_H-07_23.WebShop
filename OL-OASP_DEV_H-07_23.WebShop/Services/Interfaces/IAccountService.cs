using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.Common;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.Common;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces
{
    public interface IAccountService
    {
        Task<TokenViewModel> GetToken(LoginBinding model);
    }
}