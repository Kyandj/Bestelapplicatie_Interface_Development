using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KE03_INTDEV_SE_1_Base.Services;
using DataAccessLayer.Models;
using System.Collections.Generic;
using DataAccessLayer.Interfaces;
using System;
using System.Linq;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class AfrekenpaginaModel : PageModel
    {
        private readonly CartService _cartService;
        private readonly IOrderRepository _orderRepository;

        private readonly IProductRepository _productRepository;

        public AfrekenpaginaModel(CartService cartService, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _cartService = cartService;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }


        public List<CartItem> CartItems { get; set; } = new();

        [BindProperty]
        public string Naam { get; set; } = string.Empty;

        [BindProperty]
        public string Adres { get; set; } = string.Empty;

        public void OnGet()
        {
            CartItems = _cartService.GetCart();
        }

        private void ClearCart()
        {
            _cartService.GetType().GetMethod("ClearCart")?.Invoke(_cartService, null);
        }
        public IActionResult OnPost()
        {
            CartItems = _cartService.GetCart();

            if (!ModelState.IsValid)
                return Page();

            decimal totaalPrijs = CartItems.Sum(item => item.Price * item.Aantal);

            var customer = new Customer
            {
                Name = Naam,
                Address = Adres
            };

            var order = new Order
            {
                Customer = customer,
                OrderDate = DateTime.Now,
                Status = "Pending"
            };

            foreach (var item in CartItems)
            {
                var product = _productRepository.GetProductById(item.ProductId);
                if (product != null)
                {
                    order.Products.Add(product);
                }
            }

            // 1. Sla de order op met status Pending
            _orderRepository.AddOrder(order);

            // 2. Zet de status op Betaald en update de order
            order.Status = "Betaald";
            _orderRepository.UpdateOrder(order);

            ClearCart();

            return RedirectToPage("Afgerondpagina");
        }

    }
}
