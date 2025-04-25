using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Korisnik
    {

        [Key]
        public int ID { get; set; }

        public string? Ime { get; set; }

        public string? Prezime { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13)]
        public string? JMBG { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9)]
        public string? BrojVozacke { get; set; }

        public List<Iznajmljivanje>? IznajmljivanjaK { get; set; }
    }
}