using DakSite.Models;

namespace DakSite.Services
{
    public class TestService : BaseService
    {
        private readonly ILogger<TestService> _logger;
        private readonly IConfiguration _config;
        private readonly MySqlDBContext DBContext;

        public TestService(
            ILogger<TestService> logger,
            IConfiguration config,
            MySqlDBContext dbContext
        )
            : base(dbContext)
        {
            _logger = logger;
            _config = config;
            DBContext = dbContext;
        }

        /// <summary>
        /// 添加测试实体
        /// </summary>
        /// <param name="name">名称.</param>
        public void AddTestEntity(string name)
        {
            DBContext.TestEntities.Add(new TestEntity { Name = name });
            DBContext.SaveChanges();
        }
    }
}
