using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models;

public class Podrucje
{
    [Key]
    public int ID { get; set; }
    public string Naziv { get; set; }=null!;

    //[JsonIgnore]
    public List<Vidjena> Vidjena { get; set; } = null!;

}