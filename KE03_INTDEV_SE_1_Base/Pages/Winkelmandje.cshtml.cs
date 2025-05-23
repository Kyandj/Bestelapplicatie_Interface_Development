using KE03_INTDEV_SE_1_Base.Services;
using Microsoft.AspNetCore.Mvc;

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
                cart.Remove(item); // Verwijder het item als het aantal 0 of lager is
            }
        }
    }
    HttpContext.Session.SetObject("Cart", cart);
    return RedirectToPage();
}

