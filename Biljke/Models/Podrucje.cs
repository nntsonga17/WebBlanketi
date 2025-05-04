using System.ComponentModel.DataAnnotations;

namespace Biljke.Models;

public class Podrucje
{
    [Key]
    public int ID { get; set; } 
    [MaxLength(100)]
    public required string Naziv { get; set; }
    public List<Vidjenja>? Vidjenja { get; set; }
}