using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;

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
        public async void DelateOrder_RemovesOrderFromDb_ValidatesIfResponseIsNotNull()
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

            await buyerService.DelateOrder(order.Id);

            previusOrders = await buyerService.GetOrders(ApplicationUser);
            int newOrdersCount = previusOrders.Count;

            Assert.Equal(previusOrdersCount - 1, newOrdersCount);

        }

        [Fact]
        public async void CancelOrder_SetsStatusOfOrderToCanceled_ValidatesIfResponseIsInStatusCanceled()
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
            var previusOrder = previusOrders.FirstOrDefault(y => y.Id == order.Id);
            Assert.Equal(OrderStatus.Canceled, previusOrder.OrderStatus);


        }

        [Fact]
        public async void RegulateOrderStatus_ChangesOrderStatus_ValidatesIfResponseIsValidStatus()
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


            await buyerService.RegulateOrderStatus(order.Id, OrderStatus.Processing);
            order = await buyerService.GetOrder(order.Id);

            Assert.Equal(OrderStatus.Processing, order.OrderStatus);


        }

        [Fact]
        public async void AddBuyerFeedback_AddsBuyerFeedbackToOrder_ValidatesIfResponseIsNotNull()
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
            Assert.NotNull(order);

            var result = await buyerService.AddBuyerFeedback(new BuyerFeedbackBinding { Comment = TestString, OrderId = order.Id, Rating = 4 });
            Assert.NotNull(result);

            Assert.Equal(TestString, result.Comment);
            Assert.Equal(4, result.Rating);
            Assert.Equal(order.Id, result.OrderId);


            var feedbacks = await buyerService.GetBuyerFeedbacks(order.Id);
            Assert.Single(feedbacks);


        }

        [Fact]
        public async void DeleteBuyerFeedback_DeletesBuyerFeedbackFromOrder_ValidatesIfResponseIsEmpty()
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
            Assert.NotNull(order);

            var result = await buyerService.AddBuyerFeedback(new BuyerFeedbackBinding { Comment = TestString, OrderId = order.Id, Rating = 4 });

            await buyerService.DeleteBuyerFeedback(result.Id);

            var feedbacks = await buyerService.GetBuyerFeedbacks(order.Id);
            Assert.Empty(feedbacks);



        }

    }
}
