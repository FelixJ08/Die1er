using System.ComponentModel.DataAnnotations;

namespace Die1Er_Projektarbeit.Models
{
    public class Termin
    {
        public int ID { get; set; }

        [Required, MaxLength(200)]
        public string? Titel { get; set; }

        [Required]
        public DateTime? Datum { get; set; }

        [Required]
        public string? Beschreibung { get; set; }

        [Required]
        public Benutzer? Ersteller { get; set; }
    }
}
