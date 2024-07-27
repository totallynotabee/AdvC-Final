using Microsoft.EntityFrameworkCore;

namespace AdvC_Final.Models
{
    public class PetsContext : DbContext
    {
        public PetsContext(DbContextOptions<PetsContext> options)
            : base(options)
        {
        }

        public DbSet<Pets> Pets { get; set; }
    }
}
