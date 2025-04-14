using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class InventoryServiceProxy
    {
        private InventoryServiceProxy()
        {
            Inventory = new List<Item?>
            {
                new Item{Id = 1, Name = "Product 1"},                 
                new Item{Id = 2, Name = "Product 2"},
                new Item{Id = 3, Name = "Product 3"}
            };
        }

        private int LastKey
        {
            get
            {
                return Inventory.Any() ? Inventory.Max(p => p?.Id ?? 0) : 0;
            }
        }

        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();
        public static InventoryServiceProxy Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }

                return instance;
            }
        }

        public List<Item?> Inventory { get; private set; } = new List<Item?>();


        public Item AddOrUpdate(Item item)
        {
            if (item.Quantity < 0)
            {
                Console.WriteLine("Error: Quantity cannot be negative.");
                return null;
            }
            bool isDuplicateName = Inventory.Any(p => p.Name == item.Name && p.Id != item.Id);
            if (isDuplicateName)    //check for duplicate product names
            {
                Console.WriteLine("Duplicate product name in the inventory.");
                return null;
            }
            
            //Console.WriteLine(item.Id);
            if (item.Id == 0) // Assign unique ID for new items
            {   
                int newId = LastKey + 1;
                item.Id = newId;
                item.Product.Id = newId;    //ensure product id is the same as item id
                Inventory.Add(item);
            }
            else
            {
                var existingProduct = Inventory.FirstOrDefault(p => p.Id == item.Id);
                if (existingProduct != null)
                {
                    var index = Inventory.IndexOf(existingProduct);
                    Inventory.RemoveAt(index);
                    Inventory.Insert(index, item);
                }
                else
                {
                    Console.WriteLine("Product not found.");
                    return null;
                }
            }

            return item;
        }

        public Item? Delete(int id)
        {
            if(id == 0)
            {
                return null;
            }

            Item? item = Inventory.FirstOrDefault(p => p.Id == id);
            Inventory.Remove(item);

            return item;
        }

        public Item? GetById(int id)
        {
            return Inventory.FirstOrDefault(p => p.Id == id);
        }

    }
    
}