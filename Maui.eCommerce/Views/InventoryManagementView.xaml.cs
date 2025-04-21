using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class InventoryManagementView : ContentPage
{
    public InventoryManagementView()
    {
        InitializeComponent();
        BindingContext = new InventoryManagementViewModel();
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ProductDetails");
    }
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.Delete();
    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    
}