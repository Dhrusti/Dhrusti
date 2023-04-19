using System.ComponentModel.DataAnnotations;
namespace SalesDataChart.Models
{
    public class SalesEntity
    {
        [Key]
        public int Id { get; set; }
        public string MonthName { get; set; }
        public string TotalSales { get; set; }
    }
}
