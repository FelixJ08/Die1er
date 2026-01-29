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
        private readonly ApplicationDbContext _context;

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

            if (_context.Benutzer.Any(b => b.Email == model.Email))
            {
                ModelState.AddModelError("", "E-Mail bereits vergeben");
                return View(model);
            }

            var neuerBenutzer = new Benutzer
            {
                Nachname = model.Nachname,
                Vorname = model.Vorname,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Passwort), // ✅ FIX!
                Rolle = model.Rolle ?? "User",
                Status = "Wartend"
            };

            _context.Benutzer.Add(neuerBenutzer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(RegistrationPending));
        }

        [HttpGet]
        public IActionResult RegistrationPending()
        {
            return View();
        }
    }
}
