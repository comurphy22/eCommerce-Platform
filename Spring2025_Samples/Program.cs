using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    // Product class to handle details
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } // Available quantity in inventory
    }

    // Shopping cart item class for tracking quantities in the cart
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; } // Quantity for this item in the cart
    }

    static void Main(string[] args)
    {
        // Inventory and shopping cart collections
        var inventory = new List<Product>();
        var cart = new List<CartItem>();

        bool running = true;

        while (running)
        {
            // Main menu
            Console.WriteLine("\nWelcome to Amazon!\n");
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Create a new item (or add an item from inventory to the shopping cart)");
            Console.WriteLine("2. Read all items in the inventory or shopping cart");
            Console.WriteLine("3. Update product details in inventory (or change quantity in shopping cart)");
            Console.WriteLine("4. Delete an item from inventory (or return items from shopping cart to inventory)");
            Console.WriteLine("5. Quit and checkout");
            Console.Write("\nChoose an option: ");
            
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateProductOrCart(inventory, cart);
                    break;
                case "2":
                    ReadItems(inventory, cart);
                    break;
                case "3":
                    UpdateItems(inventory, cart);
                    break;
                case "4":
                    DeleteItems(inventory, cart);
                    break;
                case "5":
                    running = false;
                    Checkout(cart);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    // Case 1: Create a new item or add an item from inventory to the cart
    private static void CreateProductOrCart(List<Product> inventory, List<CartItem> cart)
    {
        Console.WriteLine("\nWould you like to add an item to (1) Inventory or (2) Shopping Cart?");
        var option = Console.ReadLine();

        if (option == "1")
        {
            // Add new item to inventory
            Console.Write("Enter the name of the product: ");
            var name = Console.ReadLine();

            Console.Write("Enter the price of the product: ");
            if (!decimal.TryParse(Console.ReadLine(), out var price) || price <= 0)
            {
                Console.WriteLine("Invalid price. Operation canceled.");
                return;
            }

            Console.Write("Enter the available quantity: ");
            if (!int.TryParse(Console.ReadLine(), out var quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity. Operation canceled.");
                return;
            }

            // Check if an item by this name already exists in inventory
            var existingProduct = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (existingProduct != null)
            {
                Console.WriteLine($"Product '{name}' already exists in inventory. Updating quantity!");
                existingProduct.Quantity += quantity;
            }
            else
            {
                inventory.Add(new Product { Name = name, Price = price, Quantity = quantity });
                Console.WriteLine($"Product '{name}' added to inventory.");
            }
        }
        else if (option == "2")
        {
            // Add item to shopping cart
            Console.Write("Enter the name of the product to add to the cart: ");
            var name = Console.ReadLine();

            var product = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                Console.WriteLine($"Product '{name}' not found in inventory.");
                return;
            }

            Console.Write($"Enter the quantity to add (Available: {product.Quantity}): ");
            if (!int.TryParse(Console.ReadLine(), out var cartQuantity) || cartQuantity <= 0)
            {
                Console.WriteLine("Invalid quantity. Operation canceled.");
                return;
            }

            if (cartQuantity > product.Quantity)
            {
                Console.WriteLine("Not enough stock available in inventory.");
                return;
            }

            var cartItem = cart.FirstOrDefault(c => c.Product == product);
            if (cartItem == null)
            {
                cart.Add(new CartItem { Product = product, Quantity = cartQuantity });
            }
            else
            {
                cartItem.Quantity += cartQuantity;
            }

            product.Quantity -= cartQuantity; // Reduce inventory quantity
            Console.WriteLine($"Added {cartQuantity} of '{product.Name}' to the cart.");
        }
        else
        {
            Console.WriteLine("Invalid option.");
        }
    }

    // Case 2: Read items from inventory or shopping cart
    private static void ReadItems(List<Product> inventory, List<CartItem> cart)
    {
        Console.WriteLine("\nWould you like to read from (1) Inventory or (2) Shopping Cart?");
        var option = Console.ReadLine();

        if (option == "1")
        {
            Console.WriteLine("\nInventory:");
            foreach (var product in inventory)
            {
                Console.WriteLine($"- {product.Name}: ${product.Price:F2} | Quantity: {product.Quantity}");
            }
        }
        else if (option == "2")
        {
            Console.WriteLine("\nShopping Cart:");
            foreach (var item in cart)
            {
                Console.WriteLine($"- {item.Product.Name}: ${item.Product.Price:F2} | Quantity: {item.Quantity} | Subtotal: ${item.Product.Price * item.Quantity:F2}");
            }
        }
        else
        {
            Console.WriteLine("Invalid option.");
        }
    }

    // Case 3: Update items in inventory or shopping cart
    private static void UpdateItems(List<Product> inventory, List<CartItem> cart)
    {
        Console.WriteLine("\nWould you like to update (1) Inventory or (2) Shopping Cart?");
        var option = Console.ReadLine();

        if (option == "1")
        {
            Console.Write("Enter the name of the product to update: ");
            var name = Console.ReadLine();

            var product = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                Console.WriteLine($"Product '{name}' not found in inventory.");
                return;
            }

            Console.Write("Enter the new price: ");
            if (!decimal.TryParse(Console.ReadLine(), out var price) || price <= 0)
            {
                Console.WriteLine("Invalid price. Operation canceled.");
                return;
            }

            Console.Write("Enter the new quantity: ");
            if (!int.TryParse(Console.ReadLine(), out var quantity) || quantity < 0)
            {
                Console.WriteLine("Invalid quantity. Operation canceled.");
                return;
            }

            product.Price = price;
            product.Quantity = quantity;
            Console.WriteLine($"Product '{name}' updated.");
        }
        else if (option == "2")
        {
            Console.Write("Enter the name of the product to update in the cart: ");
            var name = Console.ReadLine();

            var cartItem = cart.FirstOrDefault(c => c.Product.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (cartItem == null)
            {
                Console.WriteLine($"Product '{name}' not found in the cart.");
                return;
            }

            Console.Write($"Enter the new quantity (Current: {cartItem.Quantity}): ");
            if (!int.TryParse(Console.ReadLine(), out var quantity) || quantity < 0)
            {
                Console.WriteLine("Invalid quantity. Operation canceled.");
                return;
            }

            var adjustment = quantity - cartItem.Quantity;
            if (adjustment > cartItem.Product.Quantity)
            {
                Console.WriteLine("Not enough stock in inventory.");
                return;
            }

            cartItem.Quantity = quantity;
            cartItem.Product.Quantity -= adjustment; // Adjust inventory
            Console.WriteLine($"Cart updated. {name}: {quantity} items.");
        }
        else
        {
            Console.WriteLine("Invalid option.");
        }
    }

    // Case 4: Delete items from inventory or return items to inventory from cart
    private static void DeleteItems(List<Product> inventory, List<CartItem> cart)
    {
        Console.WriteLine("\nWould you like to delete from (1) Inventory or (2) Shopping Cart?");
        var option = Console.ReadLine();

        if (option == "1")
        {
            Console.Write("Enter the name of the product to delete from inventory: ");
            var name = Console.ReadLine();

            var product = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                Console.WriteLine($"Product '{name}' not found in inventory.");
                return;
            }

            inventory.Remove(product);
            Console.WriteLine($"Product '{name}' removed from inventory.");
        }
        else if (option == "2")
        {
            Console.Write("Enter the name of the product to return to inventory: ");
            var name = Console.ReadLine();

            var cartItem = cart.FirstOrDefault(c => c.Product.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (cartItem == null)
            {
                Console.WriteLine($"Product '{name}' not found in the cart.");
                return;
            }

            cartItem.Product.Quantity += cartItem.Quantity; // Return stock to inventory
            cart.Remove(cartItem);
            Console.WriteLine($"Product '{name}' returned to inventory.");
        }
        else
        {
            Console.WriteLine("Invalid option.");
        }
    }

    // Case 5: Quit and Checkout
    private static void Checkout(List<CartItem> cart)
    {
        Console.WriteLine("\nCheckout:");
        decimal subtotal = 0;
        foreach (var item in cart)
        {
            var itemTotal = item.Product.Price * item.Quantity;
            Console.WriteLine($"- {item.Product.Name}: ${item.Product.Price:F2} x {item.Quantity} = ${itemTotal:F2}");
            subtotal += itemTotal;
        }

        const decimal taxRate = 0.07m;
        var tax = subtotal * taxRate;
        var total = subtotal + tax;

        Console.WriteLine($"\nSubtotal: ${subtotal:F2}");
        Console.WriteLine($"Tax (7%): ${tax:F2}");
        Console.WriteLine($"Total: ${total:F2}");

        Console.WriteLine("\nThank you for shopping with Amazon!\n");
        cart.Clear(); // Empty the cart after checkout
    }
}
