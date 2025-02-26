//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Xml.Serialization;

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

            List<Product?> inventory = ProductServiceProxy.Current.Products;
            List<Product?> cart = new List<Product?>();
            
            char choice;
            int selection = -1;
            do
            {
                string? input = Console.ReadLine();
                choice = input[0];
                switch (choice)
                {
                    case 'C':
                    case 'c':
                        Console.WriteLine("Enter the name of the product to add:");
                        string productName = Console.ReadLine();

                        // Check if the product already exists in the inventory (case-insensitive check)
                        var existingProduct = inventory.FirstOrDefault(p => string.Equals(p?.Name, productName, StringComparison.OrdinalIgnoreCase));

                        if (existingProduct == null)
                        {
                            // Product doesn't exist, so add it to the inventory
                            var debugInventoryCountBefore = inventory.Count; // Debugging
        
                            var newProduct = new Product { Name = productName };
                            ProductServiceProxy.Current.AddOrUpdate(newProduct);
                            inventory.Add(newProduct);

                            Console.WriteLine($"Debug: Added {productName} | Count before: {debugInventoryCountBefore}, after: {inventory.Count}");  // Debugging
                            Console.WriteLine($"Product '{productName}' added to the inventory.");
                            existingProduct = newProduct; // Assign the newly created product
                        }
                        else
                        {
                            Console.WriteLine($"Product '{productName}' already exists in the inventory.");
                        }

                        // Allow adding multiple of the product to the shopping cart
                        Console.WriteLine("Would you like to add this product to the shopping cart? (Y/N)");
                        string response = Console.ReadLine()?.ToUpper();
                        if (response == "Y" || response == "YES")
                        {
                            cart.Add(existingProduct); // Allow duplicates in the cart
                            Console.WriteLine($"Product '{productName}' added to the shopping cart.");
                        }
                        break;
                    case 'R':
                    case 'r':  
                        Console.WriteLine("Would you like to read from the shopping cart or the inventory? (C/I)");
                        response = Console.ReadLine();
                        if (response.ToUpper() == "C")
                        {
                            cart.ForEach(Console.WriteLine);
                        }
                        else if (response.ToUpper() == "I")
                        {
                            inventory.ForEach(Console.WriteLine);
                        }
                        break;
                    case 'U':
                    case 'u': //Update the product details in the inventory (or the number of items in the shopping cart)
                        //select one of the products
                        Console.WriteLine("Would you like to update the number of items in the shopping cart (1) or the product details in the inventory (2) ?");
                        string? answer = Console.ReadLine();
                        if (answer.ToUpper() == "1")
                        {
                            //update the number of items in the shopping cart
                        }
                        else if (answer.ToUpper() == "2")
                        {
                            //update the product details in the inventory
                            Console.WriteLine("Which product would you like to update?");
                            selection = int.Parse(Console.ReadLine() ?? "-1");
                            var selectedProdInv = inventory.FirstOrDefault(p => p.Id == selection); //find the product in the inventory

                            if(selectedProdInv != null)
                            {
                                selectedProdInv.Name = Console.ReadLine() ?? "ERROR";
                                ProductServiceProxy.Current.AddOrUpdate(selectedProdInv);
                            }
                        }
                        else
                            Console.WriteLine("Error: Unknown Command");
                        break;
                    case 'D':
                    case 'd':   //Need to add feature that returns all product of a given type from the shopping cart to the inventory
                        //select one of the products
                        //throw it away
                        Console.WriteLine("Which product would you like to update?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        ProductServiceProxy.Current.Delete(selection);
                        break;
                    case 'Q':
                    case 'q':
                        break;
                    default:
                        Console.WriteLine("Error: Unknown Command");
                        break;
                }
            } while (choice != 'Q' && choice != 'q');

            Console.ReadLine();
        }
    }


}
