namespace DakSite.Services
{
    public class BaseService
    {
        private readonly MySqlDBContext DBContext;

        public BaseService(MySqlDBContext dbContext)
        {
            DBContext = dbContext;
        }
    }
}
