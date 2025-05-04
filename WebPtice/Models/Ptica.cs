using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models;

public class Ptica
{
[Key]
public int ID { get; set; }

[MaxLength(100)]
public string Naziv { get; set; } = null!;

//[RegularExpression(@"Slike\/.*.(jpg|png)")]
public string Slika { get; set; } = null!;

//[MaxLength(500)]
//public string Opis { get; set; } = null!;

//[JsonIgnore]
public List<Osobine> Osobine { get; set; } = null!;
public List<Vidjena> Vidjena { get; set; } = null!;
    
}