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
            
        public IActionResult Startseite()
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

        public IActionResult Thema(int Id)
        {
            Thema thema = _context.Thema.Where(x => x.ID == Id).FirstOrDefault();
            List<Beitrag> beitraege = [.. _context.Beitrag.Include(x=>x.Autor).Include(x => x.Thema)
                .Where(x => x.ThemaId == Id)];
            ThemaViewModel viewModel = new ThemaViewModel();
            viewModel.Beitraege = beitraege;
            viewModel.Thema = thema;
            return View(viewModel);
        }

        public IActionResult Beitrag(int id)
        {
            Beitrag beitrag = _context.Beitrag.Include(x => x.Autor)
                .Where(x => x.Id == id).FirstOrDefault();
            return View(beitrag);
        }
    }
}
