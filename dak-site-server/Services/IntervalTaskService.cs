using System.Security.Cryptography.X509Certificates;

namespace DakSite.Services
{
    public class IntervalTaskService : BackgroundService
    {
        private readonly ILogger<IntervalTaskService> _logger;
        private readonly IConfiguration _config;

        private const int AutoRunIntervalDay = 1;
        private const int AutoRunInterval_1 = 1000 * 5;
        private const int AutoRunInterval_2 = 1000 * 10;

        private Timer? AutoRunTimer_1 = null;
        private Timer? AutoRunTimer_2 = null;

        public IntervalTaskService(ILogger<IntervalTaskService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (AutoRunTimer_1 == null)
            {
                AutoRunTimer_1 = new Timer(
                    DoTask_1,
                    null,
                    TimeSpan.Zero,
                    TimeSpan.FromMilliseconds(AutoRunInterval_1)
                );
            }
            if (AutoRunTimer_2 == null)
            {
                AutoRunTimer_2 = new Timer(
                    DoTask_2,
                    null,
                    TimeSpan.Zero,
                    TimeSpan.FromMilliseconds(AutoRunInterval_2)
                );
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromDays(AutoRunIntervalDay), stoppingToken);
            }
        }

        private void DoTask_1(object? state)
        {
            string dateTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            _logger.LogInformation(
                $"【task-1】do task 1 at {dateTimeStr} for every {AutoRunInterval_1} ms"
            );
        }

        private void DoTask_2(object? state)
        {
            string dateTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            _logger.LogInformation(
                $"【task-2】do task 2 at {dateTimeStr} for every {AutoRunInterval_2} ms"
            );
        }
    }
}
