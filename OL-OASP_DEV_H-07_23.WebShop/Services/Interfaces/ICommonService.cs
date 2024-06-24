using System.Security.Claims;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces
{
    public interface ICommonService
    {
        Task AddSessionItem(string key, object value, ClaimsPrincipal user);
        Task<T> GetSessionItem<T>(string key, ClaimsPrincipal user);
        Task RemoveFromSession(string key, ClaimsPrincipal user);
        Task DeactivateAllExpiredSessions();
    }
}