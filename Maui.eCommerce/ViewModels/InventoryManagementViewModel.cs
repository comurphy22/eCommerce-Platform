using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Spring2025_Samples.Models;

namespace Maui.eCommerce.ViewModels;

public class InventoryManagementViewModel
{
    public List<Item?> Inventory
    {
        get
        {
            return InventoryServiceProxy.Current.Inventory;
        }
        
    }
}