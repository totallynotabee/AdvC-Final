using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace AdvC_Final.Areas.Account.Models
{
	public class PetsContext : DbContext
	{
		public PetsContext(DbContextOptions<PetsContext> options) : base(options) { }
		public DbSet<Pets> Pets { get; set; } = null!;
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Pets>().HasData(
                new Pets
                {
                    Id = 1,
                    ownerName = "fill",
                    petName = "name",
                    petType = "type"
                });
            

        }
	}
}
