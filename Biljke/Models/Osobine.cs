using System.ComponentModel.DataAnnotations;

namespace Biljke.Models;

public class Osobine
{
    [Key]
    public int ID { get; set; }
    
    [MaxLength(100)]
    public string? Naziv { get; set; }  

    [MaxLength(100)]
    public string? Vrednost { get; set; }  

    public List<Biljke>? Biljke { get; set; }

    public List<NepoznataBiljka>? NepoznateBiljke { get; set; }

    
    
}