using KE03_INTDEV_SE_1_Base.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
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

        public List<CartItem> CartItems { get; set; } = new();
        public decimal TotalPrice { get; set; }

        public void OnGet()
        {
            CartItems = _cartService.GetCart();
            TotalPrice = CartItems.Sum(item => item.Price * item.Aantal);
        }

        public IActionResult OnPostChangeQuantity(int productId, int change)
        {
            if (change == 1)
            {
                _cartService.AddToCart(productId, 1);
            }
            else if (change == -1)
            {
                _cartService.RemoveFromCart(productId, 1);
            }
            return RedirectToPage();
        }
    }
}
