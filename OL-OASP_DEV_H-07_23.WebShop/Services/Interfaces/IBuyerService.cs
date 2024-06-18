using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.OrderModels;
using System.Security.Claims;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces
{
    public interface IBuyerService
    {
        Task<OrderViewModel> AddOrder(OrderBinding model, ApplicationUser buyer);
        Task<OrderViewModel> AddOrder(OrderBinding model, ClaimsPrincipal user);
        Task<OrderViewModel> CancelOrder(long id);
        Task<OrderViewModel> GetOrder(long id);
        Task<List<OrderViewModel>> GetOrders();
        Task<List<OrderViewModel>> GetOrders(ApplicationUser buyer);
        Task<List<OrderViewModel>> GetOrders(ClaimsPrincipal user);
        Task<OrderViewModel> UpdateOrder(OrderUpdateBinding model);
        /// <summary>
        /// Get order by role and order id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Task<OrderViewModel> GetOrder(long id, ClaimsPrincipal user);
    }
}