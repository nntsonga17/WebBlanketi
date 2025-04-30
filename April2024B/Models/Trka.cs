using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Trka
    {
        [Key]
        public int ID { get; set; }

        public string? Lokacija { get; set; }

        public double DuzinaStaze { get; set; }

        public int BrojTakmicara { get; set; }

        public TimeSpan TrajanjeTrke { get; set; }

        public DateTime PocetakTrke { get; set; } 

        public List<Ucesce>? UcescaT { get; set; }
    }
}