using DakSite.Models;
using Microsoft.EntityFrameworkCore;

namespace DakSite
{
    public class MySqlDBContext : DbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        public MySqlDBContext(DbContextOptions<MySqlDBContext> options)
            : base(options) { }
    }
}
