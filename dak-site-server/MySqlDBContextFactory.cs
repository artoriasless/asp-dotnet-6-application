using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

// 此部分代码主要用于 migration
namespace DakSite
{
    public class MySqlDBContextFactory : IDesignTimeDbContextFactory<MySqlDBContext>
    {
        public MySqlDBContext CreateDbContext(string[] args)
        {
            string connectionString =
                "server=localhost;database=dak_site_demo;user=root;password=dak123456;";

            DbContextOptionsBuilder<MySqlDBContext> optionsBuilder =
                new DbContextOptionsBuilder<MySqlDBContext>();

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new MySqlDBContext(optionsBuilder.Options);
        }
    }
}
