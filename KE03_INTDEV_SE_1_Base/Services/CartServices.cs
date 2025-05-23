using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace KE03_INTDEV_SE_1_Base.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CartItem> GetCart()
        {
            var session = _httpContextAccessor.HttpContext!.Session;
            var json = session.GetString("Cart");

            return json != null
                ? JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>()
                : new List<CartItem>();
        }

        public void AddToCart(Product product)
        {
            var cart = GetCart();

            var existing = cart.FirstOrDefault(c => c.ProductId == product.Id);
            if (existing != null)
            {
                existing.Aantal++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Aantal = 1
                });
            }

            _httpContextAccessor.HttpContext!.Session.SetString("Cart", JsonSerializer.Serialize(cart));
        }
    }
}