using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.AccountModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.UserModel;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUserViewModel?> CreateUser(RegistrationBinding model, string role);
    }
}