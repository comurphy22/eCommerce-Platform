using Library.eCommerce.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels;

public class ProductViewModel
{
    public Item? Model { get; set; }

    public ProductViewModel()
    {
        Model = new Item();
    }
    
    public ProductViewModel(Item? model)
    {
        Model = model;
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