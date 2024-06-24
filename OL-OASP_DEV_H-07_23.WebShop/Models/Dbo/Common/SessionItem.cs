using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Base.Common;
using System.ComponentModel.DataAnnotations;

namespace OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.Common
{
    public class SessionItem: SessionItemBase, IBaseTableAtributes
    {
        [Key]
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Valid { get; set; }
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }

    }
}
