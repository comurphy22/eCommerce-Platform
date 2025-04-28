<<<<<<< HEAD
﻿using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public Product? SelectedProduct { get; set; }
        public string? Query { get; set; }
        private ProductServiceProxy _svc = ProductServiceProxy.Current;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public ObservableCollection<Product?> Products
        {
            get
            {
                var filteredList = _svc.Products.Where(p => p?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);
                return new ObservableCollection<Product?>(filteredList);
            }
        }

        public Product? Delete()
        {
            var item = _svc.Delete(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged("Products");
            return item;
        }
    }
=======
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
    public string? Query { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<Item?> Inventory
    {
        get
        {
            var filteredList = _svc.Inventory.Where(p=>p?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);
            return new ObservableCollection<Item?>(filteredList);
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
        
        var item = _svc.Delete(SelectedItem?.Id ?? 0);
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
    
    
>>>>>>> 03612078f52bbfa5d28146b9a02dc27a8115cbb9
}
