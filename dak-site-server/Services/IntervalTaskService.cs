namespace DakSite.Services
{
    public class IntervalTaskService : BackgroundService
    {
        private readonly ILogger<IntervalTaskService> _logger;
        private readonly IConfiguration _config;

        private const int AutoRunIntervalDay = 1;
        private const int AutoRunIntervalMilliSeconds_1 = 1000 * 60;
        private const int AutoRunIntervalMilliSeconds_2 = 1000 * 120;

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
                    TimeSpan.FromMilliseconds(AutoRunIntervalMilliSeconds_1)
                );
            }
            if (AutoRunTimer_2 == null)
            {
                AutoRunTimer_2 = new Timer(
                    DoTask_2,
                    null,
                    TimeSpan.Zero,
                    TimeSpan.FromMilliseconds(AutoRunIntervalMilliSeconds_2)
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
                $"【task - 1】{dateTimeStr} 每隔 {AutoRunIntervalMilliSeconds_1} 毫秒执行一次"
            );
        }

        private void DoTask_2(object? state)
        {
            string dateTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            _logger.LogInformation(
                $"【task - 2】{dateTimeStr} 每隔 {AutoRunIntervalMilliSeconds_2} 毫秒执行一次"
            );
        }
    }
}
