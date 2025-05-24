using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace KE03_INTDEV_SE_1_Base.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;

        public CartService(IHttpContextAccessor httpContextAccessor, IProductRepository productRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
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

            SaveCart(cart);
        }

        public void AddToCart(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Aantal += quantity;
            }
            else
            {
                var product = _productRepository.GetProductById(productId);
                if (product != null)
                {
                    cart.Add(new CartItem
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Aantal = quantity
                    });
                }
            }
            SaveCart(cart);
        }

        public void RemoveFromCart(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Aantal -= quantity;
                if (item.Aantal <= 0)
                {
                    cart.Remove(item);
                }
            }
            SaveCart(cart);
        }

        private void SaveCart(List<CartItem> cart)
        {
            _httpContextAccessor.HttpContext!.Session.SetString("Cart", JsonSerializer.Serialize(cart));
        }
    }
}
    
