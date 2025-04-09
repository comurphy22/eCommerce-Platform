using Library.eCommerce.Models;
using Spring2025_Samples.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private InventoryServiceProxy _invSvc = InventoryServiceProxy.Current; //manages a singleton instance of inventory class
        private List<Item> items;   //list of items in cart
        private static ShoppingCartService? instance;   //manages singleton instance of cart class
        public List<Item> CartItems //property to access items in cart
        {
            get
            {
                return items;
            }
        }
        public static ShoppingCartService Current {  //property to access singleton instance of shopping cart
            get
            {
                if(instance == null)
                {
                    instance = new ShoppingCartService();
                }

                return instance;
            } 
        }
        private ShoppingCartService() { //private constructor to prevent external instantiation
            items = new List<Item>();
        }

        public Item? AddOrUpdate(Item item) //method to add or update item in cart
        {
            var existingInvItem = _invSvc.GetById(item.Id);
            if(existingInvItem == null || existingInvItem.Quantity <= 0){  
                Console.WriteLine("Error: Item not found or out of stock.");
                return null;
            }
            
            existingInvItem.Quantity--;
            _invSvc.AddOrUpdate(existingInvItem);

            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if(existingItem == null)
            {
                var newItem = new Item(item.Name, item.Product, 1);
                CartItems.Add(newItem);
                return newItem;
            }
            else{
                //update
                existingItem.Quantity++;
                return existingItem;
            }
        }

        public Item? ReturnItem(Item? item) //method to remove item from cart
        {
            if (item?.Id <= 0 || item == null)
            {
                return null;
            }

            var itemToReturn = CartItems.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null && itemToReturn.Quantity > 0)
            {
                itemToReturn.Quantity--;
                var inventoryItem = _invSvc.GetById(itemToReturn.Id);
                if(inventoryItem != null)
                {
                    inventoryItem.Quantity++;
                    _invSvc.AddOrUpdate(inventoryItem);
                }
                else
                {
                    _invSvc.AddOrUpdate(new Item(itemToReturn.Product.Name, itemToReturn.Product, 1));
                }
            }
            
            return itemToReturn;
        }
        
        public Item? ReturnAllItems(Item? item)
        {
            if (item?.Id <= 0 || item == null)
            {
                return null;
            }

            var itemToReturn = CartItems.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null && itemToReturn.Quantity > 0)
            {
                var inventoryItem = _invSvc.GetById(itemToReturn.Id);
                if(inventoryItem != null)
                {
                    // Add all items back to inventory at once
                    inventoryItem.Quantity += itemToReturn.Quantity;
                    _invSvc.AddOrUpdate(inventoryItem);
                }
                else
                {
                    // Create new inventory item with all quantities if it doesn't exist
                    _invSvc.AddOrUpdate(new Item(itemToReturn.Product.Name, itemToReturn.Product, itemToReturn.Quantity));
                }
        
                // Set cart item quantity to 0
                itemToReturn.Quantity = 0;
            }
    
            return itemToReturn;
        }

        public void Checkout()
        {
            if (!CartItems.Any())
            {
                Console.WriteLine("Cart is empty. Nothing to checkout.");
                return;
            }

            Console.WriteLine("\n========= RECEIPT =========");
            Console.WriteLine("Amazon Store");
            Console.WriteLine("----------------------");
    
            decimal subtotal = 0;
            foreach (var item in CartItems)
            {
                decimal itemTotal = (item.Product.Price ?? 0) * item.Quantity;
                subtotal += itemTotal;
        
                Console.WriteLine($"{item.Name,-20} {item.Quantity,3} @ ${item.Product.Price,6:F2} = ${itemTotal,8:F2}");
            }

            decimal taxRate = 0.07m;
            decimal tax = subtotal * taxRate;
            decimal total = subtotal + tax;

            Console.WriteLine("----------------------");
            Console.WriteLine($"{"Subtotal:",-20} ${subtotal,8:F2}");
            Console.WriteLine($"{"Sales Tax (7%):",-20} ${tax,8:F2}");
            Console.WriteLine($"{"Total:",-20} ${total,8:F2}");
            Console.WriteLine("=========================");
            Console.WriteLine("Thank you for shopping!");
            Console.WriteLine($"Date: {DateTime.Now}");
            Console.WriteLine("=========================\n");

            // Clear the cart after checkout
            CartItems.Clear();
        }

    }
}