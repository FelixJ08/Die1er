//ReigsterBenutzerViewMOdel:
using System.ComponentModel.DataAnnotations;

namespace Die1Er_Projektarbeit.ViewModels
{
    public class RegisterBenutzerViewModel
    {
        [Required(ErrorMessage = "Email ist erforderlich")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Passwort ist erforderlich")]
        [DataType(DataType.Password)]
        public string Passwort { get; set; }

        [Required(ErrorMessage = "Vorname ist erforderlich")]
        public string Vorname { get; set; }

        [Required(ErrorMessage = "Nachname ist erforderlich")]
        public string Nachname { get; set; }

        [Required(ErrorMessage = "Rolle ist erforderlich")]
        public string Rolle { get; set; }

        [Required(ErrorMessage = "Passwort-Bestätigung ist erforderlich")]
        [DataType(DataType.Password)]
        [Compare("Passwort", ErrorMessage = "Passwörter stimmen nicht überein")]
        public string PasswortBestätigung { get; set; }

        public string? ReturnUrl { get; set; } = "/";
    }
}
