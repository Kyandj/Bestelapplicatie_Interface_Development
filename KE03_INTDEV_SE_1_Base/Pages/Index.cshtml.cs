using DataAccessLayer;
using DataAccessLayer.Models;
using KE03_INTDEV_SE_1_Base.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CartService _cartService;
        private readonly MatrixIncDbContext _context;

        public IndexModel(CartService cartService, MatrixIncDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        public List<Product> Products { get; set; } = new();

        public void OnGet()
        {
            Products = _context.Products.ToList();
        }

        public IActionResult OnPostAddToCart(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _cartService.AddToCart(product);
            }

            return RedirectToPage();
        }
    }
}
