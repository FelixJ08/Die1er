using Die1Er_Projektarbeit.Data;
using Microsoft.AspNetCore.Mvc;
using Die1Er_Projektarbeit.Models;
using Microsoft.EntityFrameworkCore;

namespace Die1Er_Projektarbeit.Controllers
{
    public class BeitragController(ApplicationDbContext context) : Controller
    {
        public readonly ApplicationDbContext _context = context;

        public IActionResult Beitrag(int id)
        {
            Beitrag beitrag = _context.Beitrag.Include(x => x.Autor)
                .Where(x => x.Id == id).FirstOrDefault();
            return View(beitrag);
        }
    }
}
