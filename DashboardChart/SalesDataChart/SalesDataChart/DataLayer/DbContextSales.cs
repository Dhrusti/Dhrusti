using Microsoft.EntityFrameworkCore;
using SalesDataChart.Models;


namespace SalesDataChart.DataLayer
{
    public class DbContextSales : DbContext
    {
        public DbContextSales(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SalesEntity> SalesData { get; set; }
    }
}
