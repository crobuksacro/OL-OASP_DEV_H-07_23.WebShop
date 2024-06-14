using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.AccountModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.UserModel;
using System.Security.Claims;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUserViewModel?> CreateUser(RegistrationBinding model, string role);
        /// <summary>
        /// Get user address
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<T> GetUserAddress<T>(ClaimsPrincipal user);
    }
}