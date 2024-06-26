using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.Common;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.AccountModels
{
    public class ApplicationUserUpdateBinding
    {
        public string Id { get; set; }
        [Display(Name = "Ime")]
        public string FirstName { get; set; }
        [Display(Name = "Prezime")]
        public string LastName { get; set; }
        public AddressUpdateBinding? Address { get; set; }
    }
}
