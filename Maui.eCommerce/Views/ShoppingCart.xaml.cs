using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.Views;

public partial class ShoppingCart : ContentPage
{
    public ShoppingCart()
    {
        InitializeComponent();
    }

    private void CancelClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}