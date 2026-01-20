using System.ComponentModel.DataAnnotations;

namespace Die1Er_Projektarbeit.Models
{
    public class Thema
    {
        public int ID { get; set; }

        [Required, MaxLength(100)]
        public string? Titel { get; set; }

        [Required]
        public Benutzer? Ersteller { get; set; }

        [Required]
        public Berufsbereich? Berufsbereich { get; set; }

        [Required]
        public DateTime? Datum { get; set; }
    }
}
