using Microsoft.EntityFrameworkCore;

namespace Models;

public class Context : DbContext
{
    public DbSet<Ptica> Ptice { get; set; } = null!;
    public DbSet<NepoznataPtica> Nepoznata { get; set; } = null!;
    public DbSet<Osobine> Osobine { get; set; } = null!;
    public DbSet<Podrucje> Podrucja { get; set; } = null!;
    public DbSet<Vidjena> Vidjenja { get; set; } = null!;

    public Context(DbContextOptions options) : base(options)
    {
        
    }
   
}