using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Die1Er_Projektarbeit.Models
{
    [PrimaryKey(nameof(Id))]
    public class Reaktion
    {
        public int Id { get; set; }

        [Required]
        public string? Typ { get; set; }

        [Required]
        public Benutzer? Benutzer { get; set; }

        [Required]
        public int BenutzerId { get; set; }

        [Required]
        public Beitrag? Beitrag { get; set; }

        [Required]
        public int BeitragId { get; set; }
    }
}
