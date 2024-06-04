namespace OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces
{
    public interface IIdentitySetup
    {
        Task CreatePlatformAdminAsync();
        Task CreateRoleAsync(string role);
    }
}