using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Ucesce
    {
        [Key]
        public int ID { get; set; }

        public Maratonac? Maratonac { get; set; }

        [JsonIgnore]
        public Trka? Trka { get; set; }

        public int StartniBroj { get; set; }

        public TimeSpan VremeIstrcano { get; set; }

        public int TrenutnaPozicija { get; set; }
    }
}