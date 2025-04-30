using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Maratonac
    {
        [Key]
        public int ID { get; set; }

        public string? Ime { get; set; }

        public string? Prezime { get; set; }

        [StringLength(13, MinimumLength = 13)]
        public string? JMBG { get; set; }

        public int BrojNagrada { get; set; }

        public double SrednjaBrzina { get; set; }

        [JsonIgnore]
        public List<Ucesce>? UcescaM { get; set; }
    }
}