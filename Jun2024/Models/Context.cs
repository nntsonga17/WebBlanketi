using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class Context : DbContext
    {
        public DbSet<Automobil> Automobili { get; set; }

        public DbSet<Korisnik> Korisnici { get; set; }

        public DbSet<Iznajmljivanje> Iznajmljivanja { get; set;}

        public Context(DbContextOptions options) : base(options)
        {
            
        }

    }
}