using Microsoft.AspNetCore.Mvc;

namespace AdvC_Final.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
