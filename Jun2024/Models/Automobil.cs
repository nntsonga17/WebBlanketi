using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Automobil{

        [Key]
        public int ID { get; set; }

        public string? Model { get; set; }

        public int PredjeniKM { get; set; }

        public int Godiste { get; set; }

        public int BrojSedista { get; set; }

        public int CenaPoDanu { get; set; }

        public bool Iznajmljen { get; set; }

        [JsonIgnore]

        public List<Iznajmljivanje>? IznajmljivanjaA { get; set; }
    }
}