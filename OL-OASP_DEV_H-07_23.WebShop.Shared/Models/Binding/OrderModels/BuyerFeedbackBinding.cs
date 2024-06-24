using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.OrderModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.OrderModels
{
    public class BuyerFeedbackBinding: BuyerFeedbackBase
    {
        public long OrderId { get; set; }
    }
}
