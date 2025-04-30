using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class Context : DbContext
    {
        public DbSet<Trka> Trke { get; set; }

        public DbSet<Maratonac> Maratonci { get; set; }

        public DbSet<Ucesce> Ucesca { get; set; }

        public Context(DbContextOptions options) : base(options){}        
    }
}