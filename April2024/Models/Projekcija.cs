using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Projekcija
    {

        [Key]
        public int ID { get; set; }

        public string? Naziv { get; set; }

        public DateTime VremePrikazivanja { get; set; }

        public string? Sifra { get; set; }

        public int BrojSale { get; set; }

        public List<Karta>? Karte { get; set; }
    }
}