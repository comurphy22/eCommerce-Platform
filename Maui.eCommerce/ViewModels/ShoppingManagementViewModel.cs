using System.Collections.ObjectModel;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels;

public class ShoppingManagementViewModel
{
    private ShoppingCartService _cartService = ShoppingCartService.Current;
    private InventoryServiceProxy _invSvc = InventoryServiceProxy.Current;

    public ObservableCollection<Item?> Inventory
    {
        get
        {
            return new ObservableCollection<Item?>(_invSvc.Inventory);
        }
    }

    public ShoppingManagementViewModel()
    {
        
    }
    
    public List<Item?> ShoppingCart
    {
        get
        {
            return _cartService.CartItems;
        }
        
    }
}