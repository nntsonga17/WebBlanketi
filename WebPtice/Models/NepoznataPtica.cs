using System.ComponentModel.DataAnnotations;

namespace Models;

public class NepoznataPtica
{
    [Key]
    public int ID { get; set; }
    public List<Osobine>? Osobine { get; set; } = null!;
}