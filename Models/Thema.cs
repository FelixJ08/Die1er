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

        public Benutzer? Ersteller { get; set; }

        public int ErstellerId { get; set;}

        public Berufsbereich? Berufsbereich { get; set; }

        public int? berufsbereichID { get; set; }

        [Required]
        public DateTime? Datum { get; set; }

        public List<Beitrag>? Beitraege { get; set; }
    }
}
