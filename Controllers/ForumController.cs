using System.Security.Cryptography.Xml;
using Die1Er_Projektarbeit.Data;
using Die1Er_Projektarbeit.Models;
using Die1Er_Projektarbeit.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Die1Er_Projektarbeit.Controllers
{
    public class ForumController(ApplicationDbContext context) : Controller
    {

        public readonly ApplicationDbContext _context = context;
            
        public IActionResult Forum()
        {
            if (_context.Thema.Count() == 0) 
            {
                var testListe = new List<Thema>();

                var testThema = new Thema();

                testThema.Datum = DateTime.Now;

                testThema.Titel = "Keine Themas vorhanden/Testlauf";

                testThema.Ersteller =
                    new Benutzer
                    {
                        ID = 1,
                        Status = "Admin",
                        Rolle = "Admin",
                        Name = "Test/Keine Themen vorhanden",
                        Email = "0",
                        Themen = new List<Thema>(),
                    };

                testThema.Ersteller.Themen.Add(testThema);
                testThema.ErstellerId = testThema.Ersteller.ID;
                testListe.Add(testThema);

                var themenListe = new List<Thema>();

                    themenListe.Add(testThema);

                var model = new ForumViewModel();

                model.Themen = themenListe;

                model.Berufsbereiche = _context.Berufsbereiches.ToList();

                return View(model);
            }
            
            //später vielleicht differenzieren ob Thema öffentlich ist?
            var datenbankListe = _context.Thema.Include(x=>x.Ersteller).ToList();

            var alternativModel = new ForumViewModel();

            alternativModel.Themen = datenbankListe;

            alternativModel.Berufsbereiche = _context.Berufsbereiches.ToList();

            return View(alternativModel);
        }


        [HttpGet]
        public IActionResult ThemaErstellen()
        {
            ThemaErstellenViewModel forumVW = new ThemaErstellenViewModel();
            List<Berufsbereich> allBerufsbereiche = _context.Berufsbereiches.ToList();

            forumVW.Berufsbereiche = allBerufsbereiche;
            return View(forumVW);
        }


        [HttpPost]
        public async Task<IActionResult> ThemaErstellen(ThemaErstellenViewModel model)  // ← ViewModel!
        {
            model.newThema.ErstellerId = 1;
            model.newThema.Datum = DateTime.Now;
            model.newThema.Berufsbereich = _context.Berufsbereiches.Where(x => x.ID == model.SelectedBerufsbereichId).FirstOrDefault();  // ← Hier zuweisen!

            _context.Thema.Add(model.newThema);
            await _context.SaveChangesAsync();
            return RedirectToAction("Forum", "Forum", new { id = model.newThema.ID });

        }
    }
}
