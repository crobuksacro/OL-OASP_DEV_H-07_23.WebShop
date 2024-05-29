using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.ProductModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.ProductModels
{
    public class ProductItemViewModel : ProductItemBase
    {
        public long Id { get; set; }
        public long? ProductCategoryId { get; set; }
        public long? QuantityTypeId { get; set; }
    }
}
