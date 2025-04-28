<<<<<<< HEAD
=======
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> 03612078f52bbfa5d28146b9a02dc27a8115cbb9
using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class InventoryManagementView : ContentPage
{
<<<<<<< HEAD
	public InventoryManagementView()
	{
		InitializeComponent();
		BindingContext = new InventoryManagementViewModel();
	}

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.Delete();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
=======
    public InventoryManagementView()
    {
        InitializeComponent();
        BindingContext = new InventoryManagementViewModel();
>>>>>>> 03612078f52bbfa5d28146b9a02dc27a8115cbb9
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }
<<<<<<< HEAD

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
=======
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.Delete();
    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void EditClicked(object? sender, EventArgs e)
    {
        var productId = (BindingContext as InventoryManagementViewModel)?.SelectedItem?.Id;
        Shell.Current.GoToAsync($"//Product?productId={productId}");
    }

    private void ContentPage_NavigatedTo(object? sender, NavigatedToEventArgs e)
>>>>>>> 03612078f52bbfa5d28146b9a02dc27a8115cbb9
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }

<<<<<<< HEAD
    private void EditClicked(object sender, EventArgs e)
    {//TODO: ?????????????
        var productId = (BindingContext as InventoryManagementViewModel)?.SelectedProduct?.Id;
        Shell.Current.GoToAsync($"//Product?productId={productId}");
    }

    private void SearchClicked(object sender, EventArgs e)
=======
    private void SearchClicked(object? sender, EventArgs e)
>>>>>>> 03612078f52bbfa5d28146b9a02dc27a8115cbb9
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }
}