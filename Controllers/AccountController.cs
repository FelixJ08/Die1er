// Account Controller
using Die1Er_Projektarbeit.Data;
using Die1Er_Projektarbeit.Models;
using Die1Er_Projektarbeit.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult AuthTabs(string? returnUrl = "/")
        {
            return View(new AuthViewModelForRegLogin
            {
                login = new LoginBenutzerViewModel { ReturnUrl = returnUrl ?? "/" },
                register = new RegisterBenutzerViewModel { ReturnUrl = returnUrl ?? "/" }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AuthTabs(AuthViewModelForRegLogin model)
        {
            if (model.IsLoginSubmission)
            {
                var benutzer = _context.Benutzer
                .FirstOrDefault(b => b.Email == model.login.Email && b.Status == "Freigegeben");

                if (benutzer == null || !BCrypt.Net.BCrypt.Verify(model.login.Passwort, benutzer.PasswordHash))
                {
                    ModelState.AddModelError("", "Ungültige Anmeldedaten oder Konto nicht freigegeben");
                    return View(model);
                }

                var claims = new List<Claim>
{
                    new Claim(ClaimTypes.NameIdentifier, benutzer.ID.ToString()),
    
                    new Claim(ClaimTypes.Name, $"{benutzer.Vorname} {benutzer.Nachname}"),
                    new Claim(ClaimTypes.Email, benutzer.Email),
                    new Claim("Rolle", benutzer.Rolle ?? "User"),
                    new Claim("Vorname", benutzer.Vorname),
                    new Claim("Nachname", benutzer.Nachname),
                    new Claim("ID", benutzer.ID.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.login.RememberMe,
                    ExpiresUtc = model.login.RememberMe ? DateTimeOffset.UtcNow.AddDays(14) : null
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Startseite", "Forum");
            }
            else
            {
                if (_context.Benutzer.Any(b => b.Email == model.register.Email))
                {
                    ModelState.AddModelError("", "E-Mail bereits vergeben");
                    return View(model);
                }

                var neuerBenutzer = new Benutzer
                {
                    Nachname = model.register.Nachname,
                    Vorname = model.register.Vorname,
                    Email = model.register.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.register.Passwort), // ✅ FIX!
                    Rolle = model.register.Rolle ?? "User",
                    Status = "Wartend"
                };

                _context.Benutzer.Add(neuerBenutzer);
                await _context.SaveChangesAsync();

                return RedirectToAction("RegistrationPending", "Account");
            }
        }








        [HttpGet]
        public IActionResult Register(string? returnUrl = "/")
        {
            return View(new RegisterBenutzerViewModel { ReturnUrl = returnUrl });
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterBenutzerViewModel model)
        //{

        //    if (_context.Benutzer.Any(b => b.Email == model.Email))
        //    {
        //        ModelState.AddModelError("", "E-Mail bereits vergeben");
        //        return View(model);
        //    }

        //    var neuerBenutzer = new Benutzer
        //    {
        //        Nachname = model.Nachname,
        //        Vorname = model.Vorname,
        //        Email = model.Email,
        //        PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Passwort), // ✅ FIX!
        //        Rolle = model.Rolle ?? "User",
        //        Status = "Wartend"
        //    };

        //    _context.Benutzer.Add(neuerBenutzer);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(RegistrationPending));
        //}

        [HttpGet]
        public IActionResult RegistrationPending()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult AuthTabs()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginBenutzerViewModel { ReturnUrl = returnUrl ?? "/" };
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginBenutzerViewModel model)
        //{
        //    var benutzer = _context.Benutzer
        //        .FirstOrDefault(b => b.Email == model.Email && b.Status == "Freigegeben");

        //    if (benutzer == null || !BCrypt.Net.BCrypt.Verify(model.Passwort, benutzer.PasswordHash))
        //    {
        //        ModelState.AddModelError("", "Ungültige Anmeldedaten oder Konto nicht freigegeben");
        //        return View(model);
        //    }

        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, benutzer.ID.ToString()),
        //        new Claim(ClaimTypes.Name, $"{benutzer.Vorname} {benutzer.Nachname}"),  
        //        new Claim(ClaimTypes.Email, benutzer.Email),
        //        new Claim("Rolle", benutzer.Rolle ?? "User"),
        //        new Claim("Vorname", benutzer.Vorname),
        //        new Claim("Nachname", benutzer.Nachname)
        //    };

        //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //           var authProperties = new AuthenticationProperties
        //    {
        //        IsPersistent = model.RememberMe,
        //        ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(14) : null
        //    };

        //    await HttpContext.SignInAsync(
        //        CookieAuthenticationDefaults.AuthenticationScheme,
        //        new ClaimsPrincipal(claimsIdentity),
        //        authProperties);

        //    return RedirectToAction("Index", "Home");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Session/Cookie komplett löschen
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Zur Startseite (oder Login)
            return RedirectToAction("Index", "Home");
        }
    }
}
