using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Biljke.Models;

public class Context : DbContext
{
    public required DbSet<Biljke> Biljke { get; set; }
    public required DbSet<Podrucje> Podrucja { get; set; }
    public required DbSet<Vidjenja> Vidjenja { get; set; }
    public required DbSet<Osobine> Osobine { get; set; }
    public required DbSet<NepoznataBiljka> NepoznateBiljke { get; set; }

    public Context(DbContextOptions options) : base(options)
    {
        
    }

}