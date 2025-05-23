using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class OrderhistorieModel : PageModel
    {
        private readonly MatrixIncDbContext _db;

        public Order LaatsteBestelling { get; set; }

        public OrderhistorieModel(MatrixIncDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            LaatsteBestelling = _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Products)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefault();
        }
    }
}
