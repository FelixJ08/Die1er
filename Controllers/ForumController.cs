using System.Security.Cryptography.Xml;
using Die1Er_Projektarbeit.Data;
using Die1Er_Projektarbeit.Models;
using Die1Er_Projektarbeit.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Die1Er_Projektarbeit.Controllers
{
    public class ForumController(ApplicationDbContext DatenbankContext) : Controller
    {

        public readonly ApplicationDbContext datenbankContext = DatenbankContext;
            
        public IActionResult Forum()
        {
            if (datenbankContext.Thema.Count() == 0) 
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

                model.Berufsbereiche = datenbankContext.Berufsbereiches.ToList();

                return View(model);
            }
            
            //später vielleicht differenzieren ob Thema öffentlich ist?
            var datenbankListe = datenbankContext.Thema.Include(x=>x.Ersteller).ToList();

            var alternativModel = new ForumViewModel();

            alternativModel.Themen = datenbankListe;

            alternativModel.Berufsbereiche = datenbankContext.Berufsbereiches.ToList();

            return View(alternativModel);
        }

        [HttpPost]
        public IActionResult ThemaErstellen(string titel, int berufsbereich)
        {
            var neuesThema = new Thema();

            neuesThema.Datum = DateTime.Now;

            //später durch Identity automatisch bestimmen
            neuesThema.ErstellerId = 2;

            neuesThema.Beitraege = new List<Beitrag>();

            neuesThema.Ersteller = datenbankContext.Benutzer.FirstOrDefault(x => x.ID == 2);

            neuesThema.Berufsbereich = datenbankContext.Berufsbereiches.FirstOrDefault(x => x.ID == berufsbereich);

            neuesThema.Titel = titel;

            datenbankContext.Thema.Add(neuesThema);
            datenbankContext.SaveChanges();

            return View("Forum");
        }
    }
}
