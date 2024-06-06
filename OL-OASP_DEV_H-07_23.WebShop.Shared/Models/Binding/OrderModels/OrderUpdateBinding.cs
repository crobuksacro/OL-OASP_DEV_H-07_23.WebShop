using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.Common;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.OrderModels
{
    public class OrderUpdateBinding : OrderBase
    {
        public long Id { get; set; }
        public AddressUpdateBinding? OrderAddress { get; set; }
        public List<OrderItemUpdateBiding>? OrderItemIds { get; set; }
    }
}
