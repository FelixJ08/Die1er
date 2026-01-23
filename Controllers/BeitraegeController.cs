using Die1Er_Projektarbeit.Models;
using Die1Er_Projektarbeit.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Die1Er_Projektarbeit.Data;
using Die1Er_Projektarbeit.Models;

namespace Die1Er_Projektarbeit.Controllers
{
    public class BeitraegeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public BeitraegeController(ILogger<HomeController> logger, ApplicationDbContext context)
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

            model.ThemaId = 1;

            _context.Beitrag.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Beitrag", "Beitraege", new { id = model.ThemaId });
        }

        public IActionResult Beitrag(int themaId)
        {
            // Das Thema wird beim klicken auf dem Button mitgegeben und muss hier bei der 1 (themaId) ersetzt werden

            Thema thema = _context.Thema.Where(x => x.ID == 1).FirstOrDefault();
            List<Beitrag> beitraege = [.. _context.Beitrag.Include(x=>x.Autor).Include(x => x.Thema)
                .Where(x => x.ThemaId == 1)];
            BeitragViewModel viewModel = new BeitragViewModel();
            viewModel.Beitraege = beitraege;
            viewModel.Thema = thema;
            return View(viewModel);
        }



    }
}
