using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.OrderModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.Common;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.OrderModels
{
    public class OrderBinding : OrderBase
    {
        public AddressBinding? OrderAddress { get; set; }
        public List<OrderItemBinding>? OrderItems { get; set; }
    }
}
