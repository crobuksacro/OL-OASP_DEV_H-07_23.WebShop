using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.ProductModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.ProductModels
{
    public class ProductCategoryViewModel : ProductCategoryBase
    {
        public long Id { get; set; }
        public List<ProductItemViewModel>? ProductItems { get; set; }
    }
}
