using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using Web_Api.OpenMeteo;
using WebApi;

namespace WebApi.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();            
            
        }
        public DbSet<User> Users { get; set; }
  

    }
}
