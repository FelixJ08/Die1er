using System.ComponentModel.DataAnnotations;

namespace Die1Er_Projektarbeit.Models
{
    public class Reaktion
    {
        public int Id { get; set; }

        [Required]
        public string? Typ { get; set; }

        [Required]
        public Benutzer? Benutzer { get; set; }

        [Required]
        public Beitrag? Beitrag { get; set; }
    }
}
