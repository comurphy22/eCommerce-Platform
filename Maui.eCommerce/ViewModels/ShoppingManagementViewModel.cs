using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels;

public class ShoppingManagementViewModel : INotifyPropertyChanged, IDisposable
{
    private ShoppingCartService _cartService = ShoppingCartService.Current;
    private InventoryServiceProxy _invSvc = InventoryServiceProxy.Current;
    private ItemViewModel _selectedItem;
    private ObservableCollection<ItemViewModel> _inventory;
    private ItemViewModel? _selectedCartItem;
    private ObservableCollection<ItemViewModel> _shoppingCart;

    public ShoppingManagementViewModel()
    {
        LoadInventory();
        LoadShoppingCart();
        _invSvc.InventoryChanged += OnInventoryChanged;
    }

    private void LoadInventory()
    {
        Inventory = new ObservableCollection<ItemViewModel>(
            _invSvc.Inventory.Select(item => new ItemViewModel(item)));
    }

    private void LoadShoppingCart()
    {
        ShoppingCart = new ObservableCollection<ItemViewModel>(
            _cartService.CartItems.Select(item => new ItemViewModel(item)));
    }

    private void OnInventoryChanged(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            LoadInventory();
        });
    }

    public ItemViewModel? SelectedCartItem
    {
        get => _selectedCartItem;
        set
        {
            if (_selectedCartItem != value)
            {
                _selectedCartItem = value;
                System.Diagnostics.Debug.WriteLine($"SelectedCartItem changed to: {value?.Model?.Product?.Name ?? "null"}");
                OnPropertyChanged();
            }
        }
    }

    public ItemViewModel SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (_selectedItem != value)
            {
                _selectedItem = value;
                System.Diagnostics.Debug.WriteLine($"SelectedItem changed to: {value?.Model?.Product?.Name ?? "null"}");
                OnPropertyChanged();
            }  
        } 
    }

    public ObservableCollection<ItemViewModel> Inventory
    {
        get => _inventory ??= new ObservableCollection<ItemViewModel>(
            _invSvc.Inventory.Select(item => new ItemViewModel(item)));
        private set
        {
            if (_inventory != value)
            {
                _inventory = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<ItemViewModel> ShoppingCart
    {
        get => _shoppingCart ??= new ObservableCollection<ItemViewModel>(
            _cartService.CartItems.Select(item => new ItemViewModel(item)));
        private set
        {
            if (_shoppingCart != value)
            {
                _shoppingCart = value;
                OnPropertyChanged();
            }
        }
    }

    public void PurchaseItem()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"PurchaseItem called, SelectedItem is: {SelectedItem?.Model?.Product?.Name ?? "null"}");
            if (SelectedItem != null)
            {
                _cartService.AddOrUpdate(SelectedItem.Model);
                LoadShoppingCart();
                LoadInventory();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in PurchaseItem: {ex.Message}");
            // Handle or propagate the error as needed
        }
    }

    public void ReturnItem()
    {
        try
        {
            if (SelectedCartItem != null)
            {
                _cartService.ReturnItem(SelectedCartItem.Model);
                LoadShoppingCart();
                LoadInventory();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in ReturnItem: {ex.Message}");
            // Handle or propagate the error as needed
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Dispose()
    {
        _invSvc.InventoryChanged -= OnInventoryChanged;
    }
}