using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class OrderhistorieModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public OrderhistorieModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> Orders { get; set; } = new();

        public void OnGet()
        {
            Orders = _orderRepository.GetAllOrders()
                .Where(o => o.Status == "Betaald")
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }


    }
}

