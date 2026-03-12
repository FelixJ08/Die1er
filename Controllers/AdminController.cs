using Die1Er_Projektarbeit.Data;
using Microsoft.AspNetCore.Mvc;

namespace Die1Er_Projektarbeit.Controllers
{
    public class AdminController(ApplicationDbContext context) : Controller
    {

        public readonly ApplicationDbContext _context = context;

        public IActionResult AdminCenter()
        {
            var wartendeNutzer = _context.Benutzer.Where(x => x.Status == "Wartend").ToList();


            return View(wartendeNutzer);
        }

        public IActionResult Auswerten(bool Auswertung, int NutzerId)
        {
            var gesuchterNutzer = _context.Benutzer.FirstOrDefault(x => x.ID == NutzerId);

            if (Auswertung == true)
            {
                gesuchterNutzer.Status = "Freigegeben";
            }
            else
            {
                gesuchterNutzer.Status = "Abgelehnt";
            }

            _context.SaveChanges();

            return RedirectToAction("AdminCenter");
        }
    }
}
