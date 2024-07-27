using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdvC_Final.Models;

namespace AdvC_Final.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly PetsContext _context;

        public HomeController(PetsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin, PetManager")]
        public IActionResult AddPet()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, PetManager")]
        [ValidateAntiForgeryToken]
        public IActionResult AddPet(Pets pet)
        {
            if (ModelState.IsValid)
            {
                _context.Pets.Add(pet);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        // Other actions as needed
    }
}
