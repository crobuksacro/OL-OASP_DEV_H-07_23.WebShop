using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.ProductModels;
using System.ComponentModel.DataAnnotations;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.ProductModels
{
    public class ProductItemUpdateBinding : ProductItemBase
    {
        public long Id { get; set; }

        [Display(Name = "Mjerna jedinica")]
        public long? QuantityTypeId { get; set; }
    }
}
