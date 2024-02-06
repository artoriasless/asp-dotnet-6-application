namespace DakSite.Services
{
    public class BaseService
    {
        private readonly MySqlDBContext DBContext;

        public BaseService(MySqlDBContext dbContext)
        {
            DBContext = dbContext;

            // 确保数据库表存在
            dbContext.Database.EnsureCreated();
        }
    }
}
