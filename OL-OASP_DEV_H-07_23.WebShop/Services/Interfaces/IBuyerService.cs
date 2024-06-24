using AutoMapper;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.OrderModels;
using System.Security.Claims;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces
{
    public interface IBuyerService
    {

        /// <summary>
        /// Delete buyer Feedback
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BuyerFeedbackViewModel> DeleteBuyerFeedback(long id);
        /// <summary>
        /// Adds buyer feedback
        /// </summary>
        /// <param name="model"></param>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<BuyerFeedbackViewModel> AddBuyerFeedback(BuyerFeedbackBinding model, ApplicationUser applicationUser);

        /// <summary>
        /// Add buyer feedback
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       Task<BuyerFeedbackViewModel> AddBuyerFeedback(BuyerFeedbackBinding model);

        /// <summary>
        /// Get by order id
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        Task<List<BuyerFeedbackViewModel>> GetBuyerFeedbacks(long orderIds);


        /// <summary>
        /// Regulate Status of order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        Task<OrderViewModel> RegulateOrderStatus(long orderId, OrderStatus orderStatus);
        /// <summary>
        /// Delate order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OrderViewModel> DelateOrder(long id);
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