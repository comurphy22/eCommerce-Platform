using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class ShoppingCart : ContentPage
{
    public ShoppingCart()
    {
        InitializeComponent();
        BindingContext = new ShoppingCartViewModel();
    }

    private void CancelClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}