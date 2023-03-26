using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using WebApi;

namespace WebApi.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            bool canconnect = Database.CanConnect();

        }

        public DbSet<Person> People { get; set; }

    }
}
