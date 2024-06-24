using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OL_OASP_DEV_H_07_23.WebShop.Data;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.Common;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;
using System.Security.Claims;
using System.Text.Json;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Implementations
{
    public class CommonService : ICommonService
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext db;

        private AppSettings appSettings;

        public CommonService(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.db = db;
            this.appSettings = appSettings.Value;
        }

        public async Task DeactivateAllExpiredSessions()
        {

            var expiredSessions = await db.SessionItems
                 .Where(y => y.Expires < DateTime.Now)
                 .ToListAsync();

            if(!expiredSessions.Any())
            {
                return;
            }

            foreach (var session in expiredSessions)
            {
                db.SessionItems.Remove(session);
            }

            await db.SaveChangesAsync();
        }


        public async Task RemoveFromSession(string key, ClaimsPrincipal user)
        {
            var applicationUser = await userManager.GetUserAsync(user);
            var dbo = await db.SessionItems
                .Include(y => y.User)
                .FirstOrDefaultAsync(y => y.Key == key
                && y.UserId == applicationUser.Id
                && y.Expires > DateTime.Now.AddHours(appSettings.ExpireSessionInHours * -1));

            if (dbo != null)
            {
                db.SessionItems.Remove(dbo);
                await db.SaveChangesAsync();
            }
        }

        public async Task AddSessionItem(string key, object value, ClaimsPrincipal user)
        {
            var applicationUser = await userManager.GetUserAsync(user);
            await RemoveFromSession(key, user);



            var dbo = new SessionItem
            {
                Key = key,
                Value = JsonSerializer.Serialize(value),
                UserId = applicationUser.Id,
                Expires = DateTime.Now.AddHours(appSettings.ExpireSessionInHours)
            };


            db.SessionItems.Add(dbo);
            await db.SaveChangesAsync();

        }

        public async Task<T> GetSessionItem<T>(string key, ClaimsPrincipal user)
        {
            var applicationUser = await userManager.GetUserAsync(user);
            var dbo = await db.SessionItems
                .Include(y => y.User)
                .FirstOrDefaultAsync(y => y.Key == key
                && y.UserId == applicationUser.Id
                && y.Expires > DateTime.Now);

            if (dbo == null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(dbo.Value);
        }

    }
}
