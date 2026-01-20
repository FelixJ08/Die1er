using System.ComponentModel.DataAnnotations;

namespace Die1Er_Projektarbeit.Models
{
    public class Benutzer
    {
        public int ID { get; set; }
         
        [Required, MaxLength(128)]
        public string? Name { get; set; }

        [Required, MaxLength(320)]
        public string? Email { get; set; }

        [Required]
        public string? Rolle { get; set; }

        [Required]
        public string? Status { get; set; }                          
    }
}