using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebApi
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) {}
		
		DbSet<Person> People { get; set; }
	}
}
