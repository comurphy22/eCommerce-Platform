using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Maui.eCommerce.ViewModels;

public class InventoryManagementViewModel : INotifyPropertyChanged
{
    private InventoryServiceProxy _svc = InventoryServiceProxy.Current;
    //InventoryManagementViewModel viewModel = new InventoryManagementViewModel();
    public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<Item?> Inventory
    {
        get
        {
            return new ObservableCollection<Item?>(_svc.Inventory);
        }
    }

    public Item? SelectedItem { get; set; }

    public Item? Add()
    {
        return null;
    }
    public Item? Delete()
    {
        if (SelectedItem is null)
        {
            return null;
        }
        
        var item = _svc.Delete(SelectedItem.Id);
        NotifyPropertyChanged(nameof(Inventory));
        return item;
    }

    public void RefreshProductList()
    {
        NotifyPropertyChanged(nameof(Inventory));
    }
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        if (propertyName is null)
        {
            throw new ArgumentNullException(nameof(propertyName));
        }
        
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
