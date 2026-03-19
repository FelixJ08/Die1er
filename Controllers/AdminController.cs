using Die1Er_Projektarbeit.Data;
using Die1Er_Projektarbeit.Models;
using Die1Er_Projektarbeit.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Die1Er_Projektarbeit.Controllers
{
    public class AdminController(ApplicationDbContext context) : Controller
    {

        public readonly ApplicationDbContext _context = context;

        public IActionResult AdminCenter()
        {
            AdminCenterViewModel viewModel = new AdminCenterViewModel();

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("AuthTabs", "Account");
            }
            viewModel.BenutzerListe = _context.Benutzer.Where(x => x.Status == "Wartend").ToList();

            viewModel.Berufsbereiche = _context.Berufsbereiches.ToList();

            return View(viewModel);
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

        [HttpPost]
        public IActionResult BerufsbereichErstellen(AdminCenterViewModel neu)
        {
            _context.Berufsbereiches.Add(neu.newBerufsbereich);
            _context.SaveChanges();

            return RedirectToAction("AdminCenter");
        }

        [HttpGet]
        public IActionResult DeleteBerufsbereich(int id)
        {
            //List<Thema> themen = _context.Thema.Where(x => x.Berufsbereich.ID == id).ToList();
            
            //themen.ForEach(x => x.Berufsbereich = null);

            var berufsbereich = _context.Berufsbereiches.Find(id);
            if (berufsbereich != null)
            {
                _context.Berufsbereiches.Remove(berufsbereich);
                _context.SaveChanges();
            }
            return RedirectToAction("AdminCenter");
        }
    }
}
