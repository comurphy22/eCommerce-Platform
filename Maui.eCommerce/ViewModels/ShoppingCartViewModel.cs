using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels;

public class ShoppingCartViewModel
{
    private readonly ShoppingCartService _cartService;

    public ShoppingCartViewModel()
    {
        _cartService=ShoppingCartService.Current;
    }
    
    public List<Item?> ShoppingCart
    {
        get
        {
            return _cartService.CartItems;
        }
        
    }
}