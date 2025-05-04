using System.ComponentModel.DataAnnotations;

namespace Biljke.Models;

public class Biljke
{
    [Key]
    public int ID { get; set; }
    
    [MaxLength(100)]
    public required string Naziv { get; set; }   

    [MaxLength(100)]
    public string? Slika { get; set; } 

    public List<Osobine>? Osobine { get; set; }
    public List<Vidjenja>? Vidjenja { get; set; }
    
}