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
        public IActionResult BeitragErstellen(int themaId)
        {
            var model = new Beitrag
            {
                ThemaId = themaId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BeitragErstellen(Beitrag model)
        {
            // Muss noch geändert werden, sobald login klappt. 
            model.AutorId = 1;
            model.Datum = DateTime.Now;  

            _context.Beitrag.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Beitrag", "Beitraege", new { id = model.ThemaId });
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
    }
}
