using Die1Er_Projektarbeit.Data;
using Microsoft.AspNetCore.Mvc;
using Die1Er_Projektarbeit.Models;
using Microsoft.EntityFrameworkCore;

namespace Die1Er_Projektarbeit.Controllers
{
    public class BeitragController(ApplicationDbContext context) : Controller
    {
        public readonly ApplicationDbContext _context = context;

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
    }
}
