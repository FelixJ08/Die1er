using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Die1Er_Projektarbeit.Models
{
    [PrimaryKey(nameof(Id))]
    public class Beitrag
    {
        public int Id { get; set; }

        [Required]
        public string? Inhalt { get; set; }

        public Benutzer? Autor { get; set; }

        public int AutorId { get; set; }

        public Thema? Thema { get; set; }

        public int? ThemaId { get; set; }

        [Required]
        public DateTime? Datum { get; set; }

        public List<Reaktion>? Reaktionen { get; set; }
    }
}
