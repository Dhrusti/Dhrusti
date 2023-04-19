using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DashboardView()
        {
            return View();
        }
    }
}
