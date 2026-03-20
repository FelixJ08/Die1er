using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Die1Er_Projektarbeit.Models
{
    [PrimaryKey(nameof(ID))]
    public class Termin
    {
        public int ID { get; set; }

        [Required, MaxLength(200)]
        public string? Titel { get; set; }

        [Required]
        public DateTime? Datum { get; set; }

        [Required]
        public string? Beschreibung { get; set; }

       
        public Benutzer? Ersteller { get; set; }

       
        public int ErstellerId { get; set; }
    }
}
