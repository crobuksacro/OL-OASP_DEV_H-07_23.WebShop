using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OL_OASP_DEV_H_07_23.WebShop.Data;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.AccountModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;
using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext db;
        private IMapper mapper;
        private SignInManager<ApplicationUser> signInManager;

        public AccountService(UserManager<ApplicationUser> userManager,
            ApplicationDbContext db, 
            IMapper mapper, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.db = db;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }

        public async Task<ApplicationUserViewModel?> CreateUser(RegistrationBinding model, string role)
        {
            var find = await userManager.FindByNameAsync(model.Email);
            if (find != null)
            {
                return null;
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                RegistrationDate = DateTime.Now
            };

            user.EmailConfirmed = true;
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                await userManager.UpdateAsync(user);
                await signInManager.SignInAsync(user, false);
                return mapper.Map<ApplicationUserViewModel>(user);
            }

            return null;
        }
        /// <summary>
        /// Get user address
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<T> GetUserAddress<T>(ClaimsPrincipal user)
        {
            var applicationUser = await userManager.GetUserAsync(user);
            var dboUser = db.Users
                .Include(x => x.Address)
                .FirstOrDefault(x => x.Id == applicationUser.Id);

            return mapper.Map<T>(dboUser.Address);

        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ApplicationUserViewModel?> GetUserProfile(ClaimsPrincipal user)
        {
            var dbo = await db.Users.Include(y => y.Address)
                .FirstOrDefaultAsync(x => x.Id == userManager.GetUserId(user));
            return mapper.Map<ApplicationUserViewModel>(user);
        }
    }
}
