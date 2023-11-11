using EventTicket.Repository.DBContext.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.Repository.DBContext
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public AppDbContext()
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
		}

		public DbSet<Entities.Category> Categories { get; set; }
		public DbSet<Entities.Event> Events { get; set; }
		public DbSet<Entities.Place> Places { get; set; }
		public DbSet<Entities.Topic> Topics { get; set; }
		public DbSet<Entities.User> Users { get; set; }
	}
}