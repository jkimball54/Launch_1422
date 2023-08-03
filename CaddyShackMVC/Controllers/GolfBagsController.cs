using Microsoft.AspNetCore.Mvc;
using CaddyShackMVC.DataAccess;
using Microsoft.EntityFrameworkCore;
using CaddyShackMVC.Models;

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
        public IActionResult New()
        {
            return View();
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

        [HttpPost]
        public IActionResult Index(GolfBag golfbag)
        {
            _context.GolfBags.Add(golfbag);
            _context.SaveChanges();

            var newGolfbagId = golfbag.Id;

            return RedirectToAction("show", new { id = golfbag.Id});
        }

        [Route("golfbags/{id:int}/edit")]
        public IActionResult Edit(int id)
        {
            var golfBag = _context.GolfBags.Include(g => g.Clubs).Where(g => g.Id == id).FirstOrDefault();
            return View(golfBag);
        }

        [HttpPost]
        [Route("golfbags/{id:int}")]
        public IActionResult Update(int id, Club club)
        {
            club.GolfBag = _context.GolfBags.Find(id);
            club.GolfBagId = _context.GolfBags.Find(id).Id;
            //Had an issue with club not having a unique id
            //so I came up with this workaround
            club.Id = _context.Clubs.Select(c => c.Id).Max() + 1;
            //might be some tech debt, but it works as is
            _context.Clubs.Add(club);
            _context.SaveChanges();

            return RedirectToAction("show", new { id = club.GolfBag.Id });
        }
    }
}
