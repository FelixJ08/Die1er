using System.ComponentModel.DataAnnotations;

namespace Die1Er_Projektarbeit.Models
{
    public class Berufsbereich
    {
        public int ID { get; set; }

        [Required, MaxLength(50)]
        public string? Bezeichnung { get; set; }

        [MaxLength(500)]
        public string? Beschreibung { get; set; }
    }
}
