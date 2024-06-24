
using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;

namespace OL_OASP_DEV_H_07_23.WebShop.Services.Implementations
{
    public class QueueProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public QueueProcessor(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var commonService = scope.ServiceProvider.GetRequiredService<ICommonService>();
                    await commonService.DeactivateAllExpiredSessions();

                }

                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
