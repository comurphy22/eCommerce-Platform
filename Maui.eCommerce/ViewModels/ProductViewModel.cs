<<<<<<< HEAD
﻿using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class ProductViewModel
    {
        public string? Name { 
            get
            {
                return Model?.Name ?? string.Empty;
            }

            set
            {
                if(Model != null && Model.Name != value)
                {
                    Model.Name = value;
                }
            }
        }

        public Product? Model { get; set; }

        public void AddOrUpdate()
        {
            ProductServiceProxy.Current.AddOrUpdate(Model);
        }

        public ProductViewModel() {
            Model = new Product();
        }

        public ProductViewModel(Product? model)
        {
            Model = model;
        }
    }
}
=======
using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels;

public class ProductViewModel
{
    private Item? cachedModel { get; set; }
    public Item? Model { get; set; }

    public void Undo()
    {
        if (cachedModel != null)  // Add null check
        {
            InventoryServiceProxy.Current.AddOrUpdate(cachedModel);
        }
    }
    public ProductViewModel()
    {
        Model = new Item();
        cachedModel = null;
    }
    
    public ProductViewModel(Item? model)
    {
        Model = model;
        if (model != null)
        {
            cachedModel = new Item(model);
        }
    }

    public string? Name
    {
        get => Model?.Name ?? string.Empty;
        set
        {
            if (Model != null && Model.Name != value)
            {
                Model.Name = value;
            }
        }
    }
    
    public int? Quantity
    {
        get
        {
            return Model?.Quantity;
        }
        set
        {
            if (Model != null && value != null && Model.Quantity != value)
            {
                Model.Quantity = value;
            }
        }
    }
    
    public decimal? Price
    {
        get => Model?.Product?.Price;
        set
        {
            if (Model?.Product != null && value.HasValue)
            {
                Model.Product.Price = value.Value;
            }
        }
    }
    
    public int Id
    {
        get => Model?.Id ?? 0;
        set
        {
            if (Model != null)
            {
                Model.Id = value;
            }
        }
    }
}
>>>>>>> 03612078f52bbfa5d28146b9a02dc27a8115cbb9
