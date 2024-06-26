﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OL_OASP_DEV_H_07_23.WebShop.Data;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.OrderModels;
using System.Security.Claims;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Implementations
{
    public class BuyerService : IBuyerService
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
        /// Get orders by user role
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<OrderViewModel>> GetOrders(ClaimsPrincipal user)
        {
            var applicationUser = await userManager.GetUserAsync(user);
            var role = await userManager.GetRolesAsync(applicationUser);

            switch (role[0])
            {
                case Roles.Admin:
                    return await GetOrders();
                case Roles.Buyer:
                    return await GetOrders(applicationUser);
                default:
                    throw new NotImplementedException($"{role[0]} isn't implemented in get orders!");

            }
        }
        /// <summary>
        /// Get orders by buyer
        /// </summary>
        /// <param name="buyer"></param>
        /// <returns></returns>
        public async Task<List<OrderViewModel>> GetOrders(ApplicationUser buyer)
        {
            var dbos = await db.Orders
                .Include(y => y.Buyer)
                 .Include(y => y.OrderItems)
                .Include(y => y.OrderAddress)
                .Include(y => y.BuyerFeedbacks)
                .Where(y => y.Valid && y.BuyerId == buyer.Id)
                .ToListAsync();

            foreach (var order in dbos)
            {
                order.BuyerFeedbacks = order.BuyerFeedbacks.Where(y => y.Valid).ToList();

            }


            return dbos.Select(y => mapper.Map<OrderViewModel>(y)).ToList();
        }
        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OrderViewModel> GetOrder(long id)
        {
            var dbo = await db.Orders
                .Include(y => y.Buyer)
                 .Include(y => y.OrderItems)
                .Include(y => y.OrderAddress)
                .FirstOrDefaultAsync(y => y.Id == id);
            return mapper.Map<OrderViewModel>(dbo);
        }
        /// <summary>
        /// Get order by role and order id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<OrderViewModel> GetOrder(long id, ClaimsPrincipal user)
        {
            var applicationUser = await userManager.GetUserAsync(user);
            var role = await userManager.GetRolesAsync(applicationUser);

            switch (role[0])
            {
                case Roles.Admin:
                    return await GetOrder(id);
                case Roles.Buyer:
                    return await GetOrder(id,applicationUser);
                default:
                    throw new NotImplementedException($"{role[0]} isn't implemented in get orders!");

            }

        }

        /// <summary>
        /// Get order by app user and orderid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buyer"></param>
        /// <returns></returns>
        public async Task<OrderViewModel> GetOrder(long id, ApplicationUser buyer)
        {
            var dbo = await db.Orders
                .Include(y => y.Buyer)
                 .Include(y => y.OrderItems)
                 .Include(y => y.BuyerFeedbacks)
                .Include(y => y.OrderAddress)
                .FirstOrDefaultAsync(y => y.Id == id && y.BuyerId == buyer.Id);


            dbo.BuyerFeedbacks = dbo.BuyerFeedbacks.Where(y => y.Valid).ToList();


            return mapper.Map<OrderViewModel>(dbo);
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrderViewModel>> GetOrders()
        {
            var dbos = await db.Orders
                .Include(y=>y.BuyerFeedbacks)
                .Include(y => y.Buyer)
                 .Include(y => y.OrderItems)
                .Include(y => y.OrderAddress)
                .Where(y => y.Valid)
                .ToListAsync();

            foreach (var order in dbos)
            {
                order.BuyerFeedbacks = order.BuyerFeedbacks.Where(y => y.Valid).ToList();

            }

            return dbos.Select(y => mapper.Map<OrderViewModel>(y)).ToList();
        }
        /// <summary>
        /// Order item
        /// </summary>
        /// <param name="model"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<OrderViewModel> AddOrder(OrderBinding model, ClaimsPrincipal user)
        {
            var applicationUser = await userManager.GetUserAsync(user);
            return await AddOrder(model, applicationUser);
        }
        /// <summary>
        /// Order item
        /// </summary>
        /// <param name="model"></param>
        /// <param name="buyer"></param>
        /// <returns></returns>
        public async Task<OrderViewModel> AddOrder(OrderBinding model, ApplicationUser buyer)
        {
            var dbo = mapper.Map<Order>(model);
            var productItems = db.ProductItems
                .Where(y => model.OrderItems.Select(y => y.ProductItemId).Contains(y.Id)).ToList();


            foreach (var product in dbo.OrderItems)
            {
                var target = productItems.FirstOrDefault(y => product.ProductItemId == y.Id);
                if (target != null)
                {
                    target.Quantity -= product.Quantity;
                    product.Price = target.Price;
                }
            }

            dbo.OrderStatus = OrderStatus.Pending;
            dbo.Buyer = buyer;
            dbo.CalcualteTotal();

            db.Orders.Add(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<OrderViewModel>(dbo);
        }
        /// <summary>
        /// Updates order
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<OrderViewModel> UpdateOrder(OrderUpdateBinding model)
        {
            var dbo = await db.Orders
                .Include(y => y.OrderItems)
                .Include(y => y.OrderAddress)
                .FirstOrDefaultAsync(y => y.Id == model.Id);
            mapper.Map(model, dbo);
            await db.SaveChangesAsync();

            return mapper.Map<OrderViewModel>(dbo);
        }
        /// <summary>
        /// Delate order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OrderViewModel> DelateOrder(long id)
        {

            await CancelOrder(id);

            var dbo = await db.Orders
                .FirstOrDefaultAsync(y => y.Id == id);

            dbo.Valid = false;
            await db.SaveChangesAsync();
            return mapper.Map<OrderViewModel>(dbo);
        }
        /// <summary>
        /// Regulate Status of order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        public async Task<OrderViewModel> RegulateOrderStatus(long orderId, OrderStatus orderStatus)
        {

            switch (orderStatus)
            {

                case OrderStatus.Canceled:
                  return  await CancelOrder(orderId);

                default:
                    var dbo = await db.Orders.FindAsync(orderId);
                    dbo.OrderStatus = orderStatus;
                    return mapper.Map<OrderViewModel>(dbo);
            }




        }
        /// <summary>
        /// Cancel Order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OrderViewModel> CancelOrder(long id)
        {
            var dbo = await db.Orders
                .Include(y => y.OrderItems)
                .ThenInclude(y => y.ProductItem)
                .FirstOrDefaultAsync(y => y.Id == id);

            var productItems = db.ProductItems
                .Where(y => dbo.OrderItems.Select(y => y.ProductItemId).Contains(y.Id)).ToList();


            foreach (var product in dbo.OrderItems)
            {
                var target = productItems.FirstOrDefault(y => product.ProductItemId == y.Id);
                if (target != null)
                {
                    target.Quantity += product.Quantity;
                }
            }

            dbo.OrderStatus = OrderStatus.Canceled;

            await db.SaveChangesAsync();
            return mapper.Map<OrderViewModel>(dbo);
        }



        /// <summary>
        /// Adds buyer feedback
        /// </summary>
        /// <param name="model"></param>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<BuyerFeedbackViewModel> AddBuyerFeedback(BuyerFeedbackBinding model, ApplicationUser applicationUser)
        {
            var order = db.Orders.FirstOrDefaultAsync(y => y.Id == model.OrderId && y.BuyerId == applicationUser.Id);
            if (order == null)
            {
                throw new Exception("Buyer isnt valid!");

            }
            var dbo = mapper.Map<BuyerFeedback>(model);
            db.BuyerFeedbacks.Add(dbo);
            db.SaveChanges();
            return mapper.Map<BuyerFeedbackViewModel>(dbo);


        }

        /// <summary>
        /// Add buyer feedback
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BuyerFeedbackViewModel> AddBuyerFeedback(BuyerFeedbackBinding model)
        {

            var dbo = mapper.Map<BuyerFeedback>(model);
            db.BuyerFeedbacks.Add(dbo);
            db.SaveChanges();
            return mapper.Map<BuyerFeedbackViewModel>(dbo);


        }

        /// <summary>
        /// Get by order id
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        public async Task<List<BuyerFeedbackViewModel>> GetBuyerFeedbacks(long orderIds)
        {

            var dbos = db.BuyerFeedbacks
                .Include(y=>y.Order)
                .Where(y => y.OrderId == orderIds && y.Valid);
          
            return dbos.Select(y => mapper.Map<BuyerFeedbackViewModel>(y)).ToList();
        }
        /// <summary>
        /// Delete buyer Feedback
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BuyerFeedbackViewModel> DeleteBuyerFeedback(long id)
        {
            var dbo = await db.BuyerFeedbacks.FindAsync(id);
            dbo.Valid = false;
            //db.BuyerFeedbacks.Remove(dbo);

            await db.SaveChangesAsync();
            return mapper.Map<BuyerFeedbackViewModel>(dbo);
        }

    }
}
