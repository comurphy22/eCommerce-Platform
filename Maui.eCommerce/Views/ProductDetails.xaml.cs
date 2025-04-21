using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;
using Spring2025_Samples.Models;

namespace Maui.eCommerce.Views;

public partial class ProductDetails : ContentPage
{
    public ProductDetails()
    {
        InitializeComponent();
        BindingContext = new ProductViewModel();
    }

    private void GoBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//InventoryManagement");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        string name = (BindingContext as ProductViewModel)?.Name;
        int quantity = (BindingContext as ProductViewModel)?.Quantity ?? 0;
        decimal price = (BindingContext as ProductViewModel)?.Price ?? 0m;

        var item = new Item
        {
            Name = name,
            Quantity = quantity,
            Product = new Product
            {
                Name = name,
                Price = price
            }
        };

        InventoryServiceProxy.Current.AddOrUpdate(item);
        Shell.Current.GoToAsync("//InventoryManagement");
    }
}