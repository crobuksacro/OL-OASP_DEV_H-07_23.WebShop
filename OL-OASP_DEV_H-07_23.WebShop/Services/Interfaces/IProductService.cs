using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.ProductModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Implementations
{
    public interface IProductService
    {
        Task<ProductCategoryViewModel> AddProductCategory(ProductCategoryBinding model);
        Task<ProductItemViewModel> AddProductItem(ProductItemBinding model);
        Task<ProductCategoryViewModel> DeleteProductCategory(long id);
        Task<ProductItemViewModel> DeleteProductItem(long id);
        Task<List<ProductCategoryViewModel>> GetProductCategories(bool? valid = true);
        Task<ProductCategoryPaginationViewModel> GetProductCategories(int page, string? searchTerm = null, int? offset = null);
        Task<ProductItemViewModel> GetProductItem(long id);
        Task<ProductCategoryViewModel> UpdateProductCategory(ProductCategoryUpdateBinding model);
        Task<ProductItemViewModel> UpdateProductItem(ProductItemUpdateBinding model);
        /// <summary>
        /// Get Product category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductCategoryViewModel> GetProductCategory(long id);
    }
}