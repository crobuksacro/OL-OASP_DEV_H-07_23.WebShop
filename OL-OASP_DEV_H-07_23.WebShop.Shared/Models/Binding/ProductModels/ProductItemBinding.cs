﻿using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.ProductModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.ProductModels
{
    public class ProductItemBinding: ProductItemBase
    {
        public long? ProductCategoryId { get; set; }
        public long? QuantityTypeId { get; set; }
    }
}
