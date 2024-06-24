using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.Common;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.UserModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.OrderModels
{
    public class OrderViewModel : OrderBase
    {
        [Display(Name = "Id narudžbe")]
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public ApplicationUserViewModel? Buyer { get; set; }
        public AddressViewModel? OrderAddress { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public List<OrderItemViewModel>? OrderItems { get; set; }
        [Required(ErrorMessage = "Total price is required.")]
        [Column(TypeName = "decimal(7, 2)")]
        [Display(Name = "Ukupno")]
        public decimal Total { get; set; }
        public List<BuyerFeedbackViewModel>? BuyerFeedbacks { get; set; }

    }
}
