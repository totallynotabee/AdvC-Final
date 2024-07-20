using AdvC_Final.Areas.Account.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace AdvC_Final.Areas.Account.Controllers
{
	[Area("Account")]
	public class HomeController : Controller
	{
		private List<Pets> getPetList()
		{
			List<Pets> pets = new List<Pets>();
			foreach (var pet in petsContext.Pets)
			{
				if (pet.ownerName == User.Identity.Name)
				{
					pets.Add(pet);
				}
			}
			return pets;
		}
		private PetsContext petsContext { get; set; }
		public HomeController(PetsContext ctx) => petsContext = ctx;
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Pets()
		{
			var pets = getPetList().OrderBy(n => n.petName).ToList();
			Console.WriteLine(pets.Count.ToString());
			return View(pets);
		}
		[HttpGet]
		public IActionResult EditPet(int id)
		{
			ViewBag.Action = "Edit";
			var pet = petsContext.Pets.Find(id);
			return View(pet);
		}
		[HttpPost]
		public IActionResult EditPet(Pets pet)
		{
			pet.ownerName = User.Identity.Name;
			if (ModelState.IsValid)
			{
				if (pet.Id == 0)
					petsContext.Pets.Add(pet);
				else
					petsContext.Pets.Update(pet);
				petsContext.SaveChanges();
				return RedirectToAction("Pets", "Home");
			}
			else
			{
				ViewBag.Action = (pet.Id == 0) ? "Add" : "Edit";
				return View(pet);
			}
		}
		[HttpGet]
		public IActionResult Update()
		{
			ViewBag.Action = "Add";
			return View("Add", new Pets());
		}
		[HttpGet]
		public IActionResult AddPet(int id)
		{
			ViewBag.Action = "Add";
			var pet = petsContext.Pets.Find(id);
			return View(pet);
		}
		[HttpPost]
		public IActionResult AddPet(Pets pet)
		{
			pet.ownerName = User.Identity.Name;
			if (ModelState.IsValid)
			{
				if (pet.Id == 0)
					petsContext.Pets.Add(pet);
				else
					petsContext.Pets.Update(pet);
				petsContext.SaveChanges();
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.Action = (pet.Id == 0) ? "Update" : "Add";
				return View(pet);
			}
		}
		[HttpGet]
		public IActionResult RemovePet(int id)
		{
			var pet = petsContext.Pets.Find(id);
			return View(pet);
		}
		[HttpPost]
		public IActionResult RemovePet(Pets pet)
		{
			petsContext.Pets.Remove(pet);
			petsContext.SaveChanges();
			return RedirectToAction("Pets", "Home");
		}

	}
}
