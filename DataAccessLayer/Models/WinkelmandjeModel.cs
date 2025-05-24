using KE03_INTDEV_SE_1_Base.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using DataAccessLayer.Models;

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

        public void OnGet()
        {
            CartItems = _cartService.GetCart();
        }
    }
}
