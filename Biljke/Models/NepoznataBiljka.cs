using System.ComponentModel.DataAnnotations;

namespace Biljke.Models;

public class NepoznataBiljka
{
    [Key]
    public int ID { get; set; }
    public List<Osobine>? Osobine { get; set; }
}