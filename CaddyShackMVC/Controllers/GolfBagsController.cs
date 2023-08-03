using Microsoft.AspNetCore.Mvc;
using CaddyShackMVC.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CaddyShackMVC.Controllers
{
    public class GolfBagsController : Controller
    {
        private readonly CaddyShackContext _context;

        public GolfBagsController(CaddyShackContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var golfBags = _context.GolfBags.ToList();
            return View(golfBags);
        }

        [Route("/golfbags/{id:int}")]
        public IActionResult Show(int id)
        {
            var golfBag = _context.GolfBags.Include(g => g.Clubs).Where(g => g.Id == id).FirstOrDefault();
            return View(golfBag);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var golfbag = _context.GolfBags.Find(id);
            _context.GolfBags.Remove(golfbag);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
