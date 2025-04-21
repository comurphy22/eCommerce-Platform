using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

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
        var name = (BindingContext as ProductViewModel)?.Name;
        InventoryServiceProxy.Current.AddOrUpdate(new Item{ Name = name});
        Shell.Current.GoToAsync("//InventoryManagement");
    }
}