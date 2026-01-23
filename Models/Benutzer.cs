using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Die1Er_Projektarbeit.Models
{

    public enum UserStatus { Wartend = 0, Aktiv = 1, Gesperrt = 2 }

    [PrimaryKey(nameof(ID))]

    public class Benutzer
    {
        public int ID { get; set; }

        [Required, MaxLength(128)]
        public string? Name { get; set; }

        [Required, MaxLength(320)]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        public string? Rolle { get; set; }

        [Required]
        public string? Status { get; set; }

        public List<Termin>? Termine { get; set; }

        public List<Beitrag>? Beitraege { get; set; }
    
        public List<Thema>? Themen { get; set; }

        public List<Reaktion>? Reaktionen { get; set; }
    }
}