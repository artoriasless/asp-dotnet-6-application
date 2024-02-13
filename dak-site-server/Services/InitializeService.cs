using Microsoft.EntityFrameworkCore;

namespace DakSite.Services
{
    /// <summary>
    /// 应用初次启动后，需要调用、执行的服务
    /// </summary>
    public class InitializeService : IHostedService
    {
        private readonly ILogger<InitializeService> _logger;
        private readonly IConfiguration _config;
        private readonly MySqlDBContext DBContext;

        public InitializeService(
            ILogger<InitializeService> logger,
            IConfiguration config,
            MySqlDBContext dbContext
        )
        {
            _logger = logger;
            _config = config;
            DBContext = dbContext;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("> 开始执行初始化任务，每次应用启动的时候会执行一次");

            // 1. 初始化数据库
            await InitDB();

            // 2. 初始化其他内容
            await InitOthers();
        }

        private async Task InitDB()
        {
            _logger.LogInformation("> 执行 migration ，保证表结构同步...");

            await Task.Run(() =>
            {
                DBContext.Database.Migrate();
            });
        }

        private async Task InitOthers()
        {
            _logger.LogInformation("> 执行其他初始化任务...");

            await Task.Run(() => {
                // another init task
            });
        }
    }
}
