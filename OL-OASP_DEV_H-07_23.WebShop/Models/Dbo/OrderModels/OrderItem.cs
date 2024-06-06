using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.OrderModels;
using System.ComponentModel.DataAnnotations;

namespace OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.OrderModels
{
    public class OrderItem : OrderItemBase, IBaseTableAtributes
    {
        [Key]
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Valid { get; set; }
        public ProductItem? ProductItem { get; set; }
        public long? ProductItemId { get; set; }

        public decimal CalculateTotal()
        {
            return Price * Quantity;
        }

    }
}
