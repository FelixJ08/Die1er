using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Die1Er_Projektarbeit.Models
{
    [PrimaryKey(nameof(ID))]
    public class Thema
    {
        public int ID { get; set; }

        [Required, MaxLength(100)]
        public string? Titel { get; set; }

        [Required]
        public Benutzer? Ersteller { get; set; }

        [Required]
        public int ErstellerId { get; set;}

        [Required]
        public Berufsbereich? Berufsbereich { get; set; }

        [Required]
        public DateTime? Datum { get; set; }

        public List<Beitrag>? Beitraege { get; set; }
    }
}
