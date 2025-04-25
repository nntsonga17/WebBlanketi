using System.Text.Json.Serialization;

namespace Models
{
    public class Iznajmljivanje
    {
        public int ID { get; set; }

        public Automobil? Automobil { get; set; }
        
        [JsonIgnore]
        public Korisnik? Korisnik { get; set; }

        public int BrojDana { get; set; }
    }
}