using Microsoft.EntityFrameworkCore;
using NewsApp.Models;

namespace NewsApp.Data
{
	public class Context : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<News> News { get; set; }

		public Context(DbContextOptions<Context> options)
		: base(options)
		{
			Database.EnsureCreated();
		}
	}
}
