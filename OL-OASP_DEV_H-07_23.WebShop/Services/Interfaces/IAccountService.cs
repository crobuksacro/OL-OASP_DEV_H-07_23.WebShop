using AutoMapper;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.AccountModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.UserModel;
using System.Security.Claims;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces
{
    public interface IAccountService
    {

        /// <summary>
        /// Update Application User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ApplicationUserViewModel> UpdateUserProfile(ApplicationUserUpdateBinding model);

        Task<ApplicationUserViewModel?> CreateUser(RegistrationBinding model, string role);
        /// <summary>
        /// Get user address
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<T> GetUserAddress<T>(ClaimsPrincipal user);
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ApplicationUserViewModel?> GetUserProfile(ClaimsPrincipal user);
        /// <summary>
        /// Get User profile
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<T> GetUserProfile<T>(ClaimsPrincipal user);
    }
}