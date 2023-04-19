using Microsoft.AspNetCore.Mvc;

namespace MVC_Project.Controllers
{
	public class AddNewSchedulingController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AddNewSchedulingPatient()
		{
			return View();
		}
	}
}
