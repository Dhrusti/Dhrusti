using Microsoft.AspNetCore.Mvc;
using SalesDataChart.DataLayer;
using SalesDataChart.Models;

namespace SalesDataChart.Controllers
{
    public class SalesRecordController : Controller
    {

        private readonly DbContextSales _context;

        public SalesRecordController(DbContextSales context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowSalesData()
        {
            return View();
        }

        [HttpPost]
        public List<Object> GetSalesData()
        {
            List<Object> data = new List<Object>();
            List<string> labels = _context.SalesData.Select(x => x.MonthName).ToList();
            data.Add(labels);

            List<string> SalesNumber = _context.SalesData.Select(x => x.TotalSales).ToList();
            data.Add(SalesNumber);

            return data;
        }
    }
}
