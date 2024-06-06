using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.OrderModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.OrderModels
{
    public class OrderItemViewModel : OrderItemBase
    {
        public long Id { get; set; }
        public long? ProductItemId { get; set; }
    }
}
