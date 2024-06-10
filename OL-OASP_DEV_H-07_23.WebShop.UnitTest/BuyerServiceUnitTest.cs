using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Services.Implementations;
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
        public async void NotNull_AddsOrderToDb_ValidatesIfResponseIsNotNull()
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

    }
}
