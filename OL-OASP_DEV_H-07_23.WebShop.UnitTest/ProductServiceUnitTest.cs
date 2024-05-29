using OL_OASP_DEV_H_07_23.WebShop.Services.Implementations;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.ProductModels;

namespace OL_OASP_DEV_H_07_23.WebShop.UnitTest
{
    public class ProductServiceUnitTest : WebShopSetup
    {
        private readonly IProductService productService;

        public ProductServiceUnitTest()
        {
            this.productService = GetProductService();
        }

        [Fact]
        public async void AddProductItem_AddsNewEntityToDb_ReturnsViewModel()
        {

            var response = await productService.AddProductItem(new ProductItemBinding
            {
                Description = TestString,
                Name = TestString,
                Price = 1233,
                ProductCategoryId = ProductCategories[1].Id,
                Quantity = 10
            });

            Assert.NotNull(response);

        }
    }
}
