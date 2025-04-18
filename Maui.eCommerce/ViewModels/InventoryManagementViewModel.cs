using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System.Collections.Generic;

namespace Maui.eCommerce.ViewModels;

public class InventoryManagementViewModel
{
    private InventoryServiceProxy _svc = InventoryServiceProxy.Current;
    public List<Item?> Inventory
    {
        get
        {
            return _svc.Inventory;
        }
        
    }

    public Item? SelectedProduct
    {
        get;
        set;
    }

    public Item? Delete()
    {
        return _svc.Delete(SelectedProduct?.Id ?? 0);
    }
}
