namespace Maui.eCommerce;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    
    private void InventoryClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//InventoryManagement"); //routing, follows singleton
    }

    private void ShopClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ShoppingCart"); //routing, follows singleton
    }
}