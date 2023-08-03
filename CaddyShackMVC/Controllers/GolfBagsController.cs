using Microsoft.AspNetCore.Mvc;
using CaddyShackMVC.DataAccess;

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
    }
}
