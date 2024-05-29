using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.Common;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.CompanyModels;
using System.ComponentModel.DataAnnotations;

namespace OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.CompanyModels
{
    public class Company : CompanyBase, IBaseTableAtributes
    {
        [Key]
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Valid { get; set; }
        public Address? Address { get; set; }
        public long? AddressId { get; set; }
        public ICollection<ProductCategory>? ProductCategorys { get; set; }
    }
}
