using KE03_INTDEV_SE_1_Base.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class WinkelmandjeModel : PageModel
    {
        private readonly CartService _cartService;

        public WinkelmandjeModel(CartService cartService)
        {
            _cartService = cartService;
        }

        // De cart die gebruikt wordt in de view
        public List<CartItem> Cart { get; private set; }

        public void OnGet()
        {
            Cart = _cartService.GetCart();
        }

        public IActionResult OnPostUpdateAantal(int productId, string action)
        {
            var cart = _cartService.GetCart();
            var item = cart.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                if (action == "increase")
                {
                    item.Aantal++;
                }
                else if (action == "decrease")
                {
                    item.Aantal--;
                    if (item.Aantal <= 0)
                    {
                        cart.Remove(item);
                    }
                }
            }

            HttpContext.Session.SetObject("Cart", cart);
            return RedirectToPage();
        }
    }
}
