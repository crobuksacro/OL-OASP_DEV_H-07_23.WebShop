﻿using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.OrderModels;

namespace OL_OASP_DEV_H_07_23.WebShop.UnitTest
{
    public class BuyerServiceUnitTest : WebShopSetup
    {
        private readonly IBuyerService buyerService;

        public BuyerServiceUnitTest()
        {
            this.buyerService = GetBuyerService();
        }
        [Fact]
        public async void GetOrder_FetchesOrderFromDb_ValidatesIfItemIsNotNull()
        {
            var result = await buyerService.GetOrder(Orders[0].Id);
            Assert.NotNull(result);
        }
        [Fact]
        public async void GetOrders_FetchesOrdersFromDb_ValidatesIfItemIsNotEmpty()
        {
            var result = await buyerService.GetOrders(ApplicationUser);
            Assert.Single(result);
        }
        [Fact]
        public async void AddOrder_AddsOrderToDb_ValidatesIfResponseIsNotNull()
        {
            var result = await buyerService.AddOrder(new Shared.Models.Binding.OrderModels.OrderBinding
            {
                Message = "Test",
                OrderAddress = new Shared.Models.Binding.Common.AddressBinding
                {
                    City = "Test",
                    Country = "Test",
                    Street = "Test",
                    Number = "Test",
                },
                OrderItems = new List<OrderItemBinding>
                    {
                        new OrderItemBinding
                        {
                            ProductItemId = ProductCategories[0].ProductItems.First().Id,
                            Quantity = 10
                        }
                    }

            }, ApplicationUser);
            Assert.NotNull(result);
        }
        [Fact]
        public async void UpdateOrder_UpdatesOrderToDb_ValidatesIfResponseIsNotEqualToPreviusRecord()
        {
            var order = await buyerService.AddOrder(new Shared.Models.Binding.OrderModels.OrderBinding
            {
                Message = "Test",
                OrderAddress = new Shared.Models.Binding.Common.AddressBinding
                {
                    City = "Test",
                    Country = "Test",
                    Street = "Test",
                    Number = "Test",
                },
                OrderItems = new List<OrderItemBinding>
                    {
                        new OrderItemBinding
                        {
                            ProductItemId = ProductCategories[0].ProductItems.First().Id,
                            Quantity = 10
                        }
                    }

            }, ApplicationUser);

            var result = await buyerService.UpdateOrder
                (
                    new OrderUpdateBinding
                    {
                        Id = order.Id,
                        Message = "Test2",
                        OrderAddress = new Shared.Models.Binding.Common.AddressUpdateBinding
                        {
                            City = "Test2",
                            Country = "Test2",
                            Street = "Test2",
                            Number = "Test2",
                            Id = order.OrderAddress.Id
                        }
                    }

                );

            Assert.NotEqual(order.Message, result.Message);
            Assert.NotEqual(order.OrderAddress.Country, result.OrderAddress.Country);
        }
        [Fact]
        public async void CancelOrder_RemovesOrderFromDb_ValidatesIfResponseIsNotNull()
        {
            var order = await buyerService.AddOrder(new Shared.Models.Binding.OrderModels.OrderBinding
            {
                Message = "Test",
                OrderAddress = new Shared.Models.Binding.Common.AddressBinding
                {
                    City = "Test",
                    Country = "Test",
                    Street = "Test",
                    Number = "Test",
                },
                OrderItems = new List<OrderItemBinding>
                    {
                        new OrderItemBinding
                        {
                            ProductItemId = ProductCategories[0].ProductItems.First().Id,
                            Quantity = 10
                        }
                    }

            }, ApplicationUser);
            var previusOrders = await buyerService.GetOrders(ApplicationUser);
            int previusOrdersCount = previusOrders.Count;

            await buyerService.CancelOrder(order.Id);

            previusOrders = await buyerService.GetOrders(ApplicationUser);
            int newOrdersCount = previusOrders.Count;

            Assert.Equal(previusOrdersCount-1, newOrdersCount);

        }
    }
}
