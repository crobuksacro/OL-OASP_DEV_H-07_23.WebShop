using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.OrderModels;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.OrderModels
{
    public class BuyerFeedbackViewModel: BuyerFeedbackBase
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
    }
}
