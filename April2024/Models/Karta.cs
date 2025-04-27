using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Karta
    {

        [Key]
        public int ID { get; set; }

        public int Red { get; set; }

        public int BrSedista { get; set; }

        public double Cena { get; set; }

        public bool Kupljena  {get; set; }

        [JsonIgnore]
        public Projekcija? Projekcija { get; set; }
    }
}