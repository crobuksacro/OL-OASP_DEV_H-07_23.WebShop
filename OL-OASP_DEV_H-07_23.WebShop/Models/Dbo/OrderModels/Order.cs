using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.Common;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.OrderModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;

namespace OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.OrderModels
{
    public class Order : OrderBase, IBaseTableAtributes
    {
        [Key]
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Valid { get; set; }
        [Required(ErrorMessage = "Total price is required.")]
        [Column(TypeName = "decimal(7, 2)")]
        public decimal Total { get; set; }
        public ApplicationUser? Buyer { get; set; }
        public string? BuyerId { get; set; }
        public Address? OrderAddress { get; set; }
        public long? OrderAddressId { get; set; }
        public OrderStatus? OrderStatus { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }

        public void CalcualteTotal()
        {
            if (OrderItems == null)
            {
                return;
            }

            Total = OrderItems.Select(y => y.CalculateTotal()).Sum();
        }

    }
}
