using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KE03_INTDEV_SE_1_Base.Services;
using DataAccessLayer.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class AfrekenpaginaModel : PageModel
    {
        private readonly CartService _cartService;
        private readonly MatrixIncDbContext _db;

        public List<CartItem> Cart { get; set; } = new();

        [BindProperty]
        public string Naam { get; set; } = "";

        [BindProperty]
        public string Adres { get; set; } = "";

        public AfrekenpaginaModel(CartService cartService, MatrixIncDbContext db)
        {
            _cartService = cartService;
            _db = db;
        }

        public void OnGet()
        {
            Cart = _cartService.GetCart();
        }

        public IActionResult OnPost()
        {
            Cart = _cartService.GetCart();
            if (!ModelState.IsValid || Cart.Count == 0)
            {
                return Page();
            }

            // 1. Zoek bestaande klant of maak nieuwe aan
            var customer = _db.Customers.FirstOrDefault(c => c.Name == Naam && c.Address == Adres);
            if (customer == null)
            {
                customer = new Customer { Name = Naam, Address = Adres, Active = true };
                _db.Customers.Add(customer);
                _db.SaveChanges();
            }

            // 2. Maak nieuwe order aan
            var order = new Order
            {
                CustomerId = customer.Id,
                OrderDate = DateTime.Now
            };

            // 3. Voeg producten toe aan de order
            foreach (var item in Cart)
            {
                var product = _db.Products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    // Voeg het product meerdere keren toe als het aantal > 1 is
                    for (int i = 0; i < item.Aantal; i++)
                    {
                        order.Products.Add(product);
                    }
                }
            }

            _db.Orders.Add(order);
            _db.SaveChanges();

            // 4. Winkelmandje legen
            HttpContext.Session.Remove("Cart");

            // 5. Redirect naar afrondpagina
            return RedirectToPage("/Afgerondpagina");
        }
    }
}
