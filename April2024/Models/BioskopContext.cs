using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class BioskopContext : DbContext
    {
        public DbSet<Projekcija> Projekcije { get; set; }
        public DbSet<Karta> Karte { get; set; }

        public BioskopContext(DbContextOptions options) : base(options)
        {}
    }
}