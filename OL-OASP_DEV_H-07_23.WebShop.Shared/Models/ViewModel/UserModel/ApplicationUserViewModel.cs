using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.Common;
using System.ComponentModel.DataAnnotations;

namespace OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.UserModel
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Ime")]
        public string FirstName { get; set; }
        [Display(Name = "Prezime")]
        public string LastName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public AddressViewModel? Address { get; set; }
    }
}
