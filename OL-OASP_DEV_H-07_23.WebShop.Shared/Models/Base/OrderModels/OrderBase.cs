using System.ComponentModel.DataAnnotations;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.OrderModels
{
    public abstract class OrderBase
    {
        [Display(Name = "Poruka")]
        public string? Message { get; set; }

    }
}
