using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring2025_Samples.Models
{
    public class Product
    {
        public int Id { get; set; }
<<<<<<< HEAD

        public string? Name { get; set; }
=======
        public string? Name { get; set; }
        //public int Quantity { get; set;  only for cart items?
        
        public decimal? Price { get; set; }
>>>>>>> 03612078f52bbfa5d28146b9a02dc27a8115cbb9

        public string? Display
        {
            get
            {
                return $"{Id}. {Name}";
            }
        }

        public Product()
        {
            Name = string.Empty;
        }
<<<<<<< HEAD

        public override string ToString()
        {
            return Display ?? string.Empty;
=======
        
        public Product(Product p)
        {
            Id = p.Id;
            Name = p.Name;
            Price = p.Price;
        }

        public Product(int id, string name, decimal? price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Id}. {Name} - {Price}";
>>>>>>> 03612078f52bbfa5d28146b9a02dc27a8115cbb9
        }
    }
}
