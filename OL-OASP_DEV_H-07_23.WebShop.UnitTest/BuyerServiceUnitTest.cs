using OL_OASP_DEV_H_07_23.WebShop.Services.Implementations;

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
    }
}
