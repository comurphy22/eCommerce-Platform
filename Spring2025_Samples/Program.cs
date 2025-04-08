using System;
using System.Collections.Generic;
using System.Linq;
using Library.eCommerce.Services;
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
                        Console.WriteLine("Enter product name:");
                        ProductServiceProxy.Current.AddOrUpdate(new Product
                        {
                            Name = Console.ReadLine()
                        });
                        Console.WriteLine("Created new item");
                        break;
                    case 'R':
                    case 'r':
                        list.ForEach(Console.WriteLine);
                        break;
                    case 'U':
                    case 'u':
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
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
                        break;
                    case 'D':
                    case 'd':
                        Console.WriteLine("Which product would you like to delete?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        ProductServiceProxy.Current.Delete(selection);
                        Console.WriteLine("Deleted item");
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