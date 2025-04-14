using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class InventoryManagementView : ContentPage
{
    public InventoryManagementView()
    {
        InitializeComponent();
        BindingContext = new InventoryManagementViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}