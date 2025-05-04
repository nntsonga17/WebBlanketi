using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models;

public class Osobine
{
    [Key]
    public int ID { get; set; }
    public string Naziv { get; set; }=null!;
    public string Vrednost { get; set; }=null!;

    public required bool ViseVrednosti { get; set; }
    
    [JsonIgnore]
    public List<Ptica> Ptica { get; set; }=null!;
    public List<NepoznataPtica> Nepoznata { get; set;}=null!;
}