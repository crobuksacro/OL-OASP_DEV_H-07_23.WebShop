using Microsoft.AspNetCore.Identity;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.Common;

namespace OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address? Address { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string? RefreshToken { get;  set; }
        public DateTime? RefreshTokenExpiryTime { get;  set; }
    }
}
