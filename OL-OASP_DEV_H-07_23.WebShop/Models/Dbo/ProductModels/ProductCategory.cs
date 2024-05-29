using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.CompanyModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.ProductModels;
using System.ComponentModel.DataAnnotations;

namespace OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.ProductModels
{
    public class ProductCategory : ProductCategoryBase, IBaseTableAtributes
    {
        [Key]
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Valid { get; set; }
        public ICollection<ProductItem>? ProductItems { get; set; }
        public Company? Company { get; set; }
        public long? CompanyId { get; set; }
    }
}
