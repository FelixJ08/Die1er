// Account Controller
using Die1Er_Projektarbeit.Data;
using Die1Er_Projektarbeit.Models;
using Die1Er_Projektarbeit.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Claims;

namespace Die1Er_Projektarbeit.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context; // Dein MSSQL DbContext

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register(string? returnUrl = "/")
        {
            return View(new RegisterBenutzerViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterBenutzerViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.Benutzer.Any(b => b.Email == model.Email))
            {
                ModelState.AddModelError("", "Benutzername ist bereits vergeben");
                return View(model);
            }

            var neuerBenutzer = new Benutzer
            {
                Nachname = model.Nachname,
                Vorname = model.Vorname,
                Email = model.Email,
                PasswordHash = model.Passwort,
                Rolle = model.Rolle,
                Status = "Wartend"
            };

            _context.Benutzer.Add(neuerBenutzer);
            await _context.SaveChangesAsync();

            // KEIN Login!
            // Zeige Screen "Wird geprüft"
            return RedirectToAction(nameof(RegistrationPending));
        }

        [HttpGet]
        public IActionResult RegistrationPending()
        {
            return View();
        }
    }
}
