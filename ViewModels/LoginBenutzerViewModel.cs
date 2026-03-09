using System.ComponentModel.DataAnnotations;

public class LoginBenutzerViewModel
{
    [Required(ErrorMessage = "E-Mail ist erforderlich")]
    [EmailAddress(ErrorMessage = "Ungültige E-Mail Adresse")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Passwort ist erforderlich")]
    [DataType(DataType.Password)]
    public string Passwort { get; set; } = "";

    public string? ReturnUrl { get; set; } = "/";

    public bool RememberMe { get; set; }
}