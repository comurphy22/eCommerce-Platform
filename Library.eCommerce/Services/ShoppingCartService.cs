using Library.eCommerce.Models;
using Spring2025_Samples.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current; //manages a singleton instance of inventory class
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
            var existingInvItem = _prodSvc.GetById(item.Id);
            if(existingInvItem == null) {   //add quantity later?
                return null;
            }

            /*if (existingInvItem != null)
            {
                existingInvItem.Quantity--;
            }*/

            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if(existingItem == null)
            {
                //add
                var newItem = new Item(item);
                newItem.Quantity = 1;
                CartItems.Add(newItem);
            }
            else{
                //update
                existingItem.Quantity++;
            }
            return existingInvItem == null ? null : new Item { Id = existingInvItem.Id, Product = existingInvItem, Quantity = 1 };
        }

        public Item? ReturnItem(Item? item) //method to remove item from cart
        {
            if (item?.Id <= 0 || item == null)
            {
                return null;
            }

            var itemToReturn = CartItems.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null)
            {
                itemToReturn.Quantity--;
                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id); ;
                if(inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Product(itemToReturn.Product));
                }

                /*else
                {
                 invenvtoryItem.Quantity++;
                }*/
            }
            
            return itemToReturn;
        }

    }
}