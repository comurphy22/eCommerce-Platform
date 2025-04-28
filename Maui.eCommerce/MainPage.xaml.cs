using Maui.eCommerce.ViewModels;

<<<<<<< HEAD
namespace Maui.eCommerce
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void InventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }
    }

}
=======
namespace Maui.eCommerce;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel(); 
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
>>>>>>> 03612078f52bbfa5d28146b9a02dc27a8115cbb9
