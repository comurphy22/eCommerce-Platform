using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels;

public class ShoppingManagementViewModel : INotifyPropertyChanged
{
    private ShoppingCartService _cartService = ShoppingCartService.Current;
    private InventoryServiceProxy _invSvc = InventoryServiceProxy.Current;
    private ItemViewModel _selectedItem;
    private ObservableCollection<ItemViewModel> _inventory;

    public event PropertyChangedEventHandler? PropertyChanged;
    public ItemViewModel SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (_selectedItem != value)
            {
                _selectedItem = value;
                System.Diagnostics.Debug.WriteLine($"SelectedItem changed to: {value?.Model?.Product?.Name ?? "null"}");
                OnPropertyChanged(nameof(SelectedItem));
            }  
        } 
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ItemViewModel? SelectedCartItem { get; set; }
    public ObservableCollection<ItemViewModel> Inventory
    {
        get
        {
            if (_inventory == null)
            {
                _inventory = new ObservableCollection<ItemViewModel>(
                    _invSvc.Inventory.Select(item => new ItemViewModel(item))
                );
            }
            return _inventory;
        }
    }

    public void PurchaseItem()
    {
        System.Diagnostics.Debug.WriteLine($"PurchaseItem called, SelectedItem is: {SelectedItem?.Model?.Product?.Name ?? "null"}");
        if (SelectedItem != null)
        {
            _cartService.AddOrUpdate(SelectedItem.Model);
            _shoppingCart = new ObservableCollection<Item>(_cartService.CartItems);
            OnPropertyChanged(nameof(ShoppingCart));
        }
    }
    
    public ShoppingManagementViewModel()
    {
        
    }
    
    private ObservableCollection<Item> _shoppingCart;
    public ObservableCollection<Item> ShoppingCart
    {
        get
        {
            if (_shoppingCart == null)
            {
                _shoppingCart = new ObservableCollection<Item>(_cartService.CartItems);
            }
            return _shoppingCart;
        }
        private set
        {
            if (_shoppingCart != value)
            {
                _shoppingCart = value;
                OnPropertyChanged(nameof(ShoppingCart));
            }
        }
    }
}
