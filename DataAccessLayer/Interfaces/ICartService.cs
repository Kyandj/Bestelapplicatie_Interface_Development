using DataAccessLayer.Models;
using System.Collections.Generic;

namespace KE03_INTDEV_SE_1_Base.Services
{
    public interface ICartService
    {
        List<CartItem> GetCart();
        void AddToCart(Product product);
    }
}
