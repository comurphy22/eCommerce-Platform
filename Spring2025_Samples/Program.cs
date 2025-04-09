using System;
using System.Collections.Generic;
using System.Linq;
using Library.eCommerce.Services;
using Library.eCommerce.Models;
using Spring2025_Samples.Models;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Amazon!");

            Console.WriteLine("C. Create new inventory item");
            Console.WriteLine("R. Read all inventory items");
            Console.WriteLine("U. Update an inventory item");
            Console.WriteLine("D. Delete an inventory item");
            Console.WriteLine("Q. Quit");

            List<Product?> list = ProductServiceProxy.Current.Products;
            List<Item> items = ShoppingCartService.Current.CartItems;
            var option = "";
            int selection;
            
            while (true)
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                char choice = input[0];
                if (choice == 'Q' || choice == 'q')
                    break;

                switch (choice)
                {
                    case 'C':
                    case 'c':
                        Console.WriteLine("Would you like to add item to (1) Inventory or (2) Shopping Cart?");
                        option = Console.ReadLine();

                        if (option == "1")
                        {
                            Console.WriteLine("Enter product name:");
                            var name = Console.ReadLine();
                            Console.WriteLine("Enter product price:");
                            var price = decimal.Parse(Console.ReadLine());
                            //Console.WriteLine("Enter product quantity:");
                            //var quantity = int.Parse(Console.ReadLine());
                            
                            ProductServiceProxy.Current.AddOrUpdate(new Product
                            {
                                Name = name,
                                Price = price,
                                //Quantity = quantity
                            });
                        }
                        else if (option == "2")
                        {
                            Console.WriteLine("Enter product name to add to shopping cart:");
                            var productName = Console.ReadLine();

                            // Step 1: Find the product in the inventory
                            var inventoryProduct = ProductServiceProxy.Current.Products
                                .FirstOrDefault(p => p != null && p.Name != null && p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

                            if (inventoryProduct != null)                           
                            {
                                // Step 2: Add it to the cart if it exists in the inventory
                                var addedItem = ShoppingCartService.Current.AddOrUpdate(new Item
                                {
                                    Id = inventoryProduct.Id,
                                    Product = inventoryProduct,
                                    Quantity = 1 // Adding one item to the cart
                                });

                                if (addedItem != null)
                                {
                                    Console.WriteLine(addedItem);
                                    Console.WriteLine("Added item to shopping cart.");
                                }
                                else
                                {
                                    Console.WriteLine("Error: Failed to add item to cart.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error: Product does not exist in inventory or is out of stock.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Unknown option");
                        }
                        
                        break;
                    case 'R':
                    case 'r':
                        Console.WriteLine("Would you read items from (1) Inventory or (2) Shopping Cart?");
                        option = Console.ReadLine();
                        if (option == "1")
                        {
                            list.ForEach(Console.WriteLine);
                        }
                        else if (option == "2")
                        {
                            items.ForEach(Console.WriteLine);
                        }
                        break;
                    case 'U':
                    case 'u':
                        Console.WriteLine("Would you like to update (1) Inventory or (2) Shopping Cart?");
                        option = Console.ReadLine();
                        if (option == "1")
                        {
                            Console.WriteLine("Which product would you like to update?");
                            selection = int.Parse(Console.ReadLine() ?? "-1");
                            var selectedProd = list.FirstOrDefault(p => p.Id == selection);

                            if (selectedProd != null)
                            {
                                Console.WriteLine("Enter new name:");
                                selectedProd.Name = Console.ReadLine() ?? "ERROR";
                                ProductServiceProxy.Current.AddOrUpdate(selectedProd);
                                Console.WriteLine("Updated item");
                            }
                            else
                            {
                                Console.WriteLine("Product not found.");
                            }
                        }
                        else if (option == "2") //update shopping cart
                        {
                            Console.WriteLine("Which product would you like to update?");
                            selection = int.Parse(Console.ReadLine() ?? "-1");
                            var selectedProd = items.FirstOrDefault(p => p.Id == selection);

                            if (selectedProd != null)
                            {
                                Console.WriteLine("Enter new quantity:");
                                selectedProd.Quantity = int.Parse(Console.ReadLine() ?? "0");
                                if (selectedProd.Quantity <= 0)
                                {
                                    Console.WriteLine("Quantity must be greater than 0");
                                    break;
                                }
                                ShoppingCartService.Current.AddOrUpdate(selectedProd);
                                Console.WriteLine("Updated item with new quantity {selectedProd.Quantity}");
                                
                            }
                        }
                        break;
                    case 'D':
                    case 'd':
                        Console.WriteLine("Would you like to delete (1) Inventory or (2) Shopping Cart (remove all items from cart)?");
                        option = Console.ReadLine();
                        if (option == "1")
                        {
                            Console.WriteLine("Which product would you like to delete?");
                            selection = int.Parse(Console.ReadLine() ?? "-1");
                            ProductServiceProxy.Current.Delete(selection);
                            Console.WriteLine("Deleted item");
                        }
                        else if (option == "2")
                        {
                            Console.WriteLine("Which product would you like to delete?");
                            selection = int.Parse(Console.ReadLine() ?? "-1");
                            var selectedProd = items.FirstOrDefault(p => p.Id == selection);
                            ShoppingCartService.Current.ReturnItem(selectedProd);
                            Console.WriteLine("Deleted item");
                            //items.Remove(items.FirstOrDefault(p => p.Id == selection));
                            //items.ForEach(Console.WriteLine)
                        }
                        
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            }

            Console.WriteLine("Exiting application...");
        }
    }
}