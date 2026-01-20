using System.ComponentModel.DataAnnotations;

namespace Die1Er_Projektarbeit.Models
{
    public class Beitrag
    {
        public int Id { get; set; }

        [Required]
        public string? Inhalt { get; set; }

        [Required]
        public Benutzer? Autor { get; set; }

        [Required]
        public Thema? Thema { get; set; }

        [Required]
        public DateTime? Datum { get; set; }
    }
}
