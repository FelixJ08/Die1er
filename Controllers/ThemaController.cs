using Die1Er_Projektarbeit.Data;
using Die1Er_Projektarbeit.Models;
using Die1Er_Projektarbeit.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Die1Er_Projektarbeit.Controllers
{
    public class ThemaController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public ThemaController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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
        public async Task<IActionResult> ThemaErstellen(ThemaErstellenViewModel model)
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
