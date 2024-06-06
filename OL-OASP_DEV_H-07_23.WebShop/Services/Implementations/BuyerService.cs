using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OL_OASP_DEV_H_07_23.WebShop.Data;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.OrderModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Implementations
{
    public class BuyerService
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext db;
        private IMapper mapper;

        public BuyerService(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IMapper mapper)
        {
            this.userManager = userManager;
            this.db = db;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrderViewModel>> GetOrders()
        {
            var dbo = await db.Orders
                .Include(y => y.Buyer)
                 .Include(y => y.OrderItems)
                .Include(y => y.OrderAddress)
                .Where(y => y.Valid)
                .ToListAsync();

            return dbo.Select(y => mapper.Map<OrderViewModel>(y)).ToList();
        }

    }
}
