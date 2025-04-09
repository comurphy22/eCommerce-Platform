using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy()
        {
            Products = new List<Product?>();
        }

        private int LastKey
        {
            get
            {
                if(!Products.Any())
                {
                    return 0;
                }

                return Products.Select(p => p?.Id ?? 0).Max();
            }
        }

        private static ProductServiceProxy? instance;
        private static object instanceLock = new object();
        public static ProductServiceProxy Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductServiceProxy();
                    }
                }

                return instance;
            }
        }

        public List<Product?> Products { get; private set; }


        public Product AddOrUpdate(Product product)
        {
            bool isDuplicateName = Products.Any(p => p.Name == product.Name && p.Id != product.Id);
            if (isDuplicateName)
            {
                Console.WriteLine("Duplicate product name in the inventory.");
                return null;
            }
            if(product.Id == 0) //if product id is 0, add new product
            {
                product.Id = LastKey + 1;
                //product.Id = 1;
                Products.Add(product);
            }
            else    //update
            {
                var existingProduct = Products.FirstOrDefault(p => p.Id == product.Id);
                if (existingProduct != null)
                {
                    var index = Products.IndexOf(existingProduct);
                    Products.RemoveAt(index);
                    Products.Insert(index, product);
                }
                else
                {
                    Console.WriteLine("Product not found.");
                    return null;
                }
            }
            
            return product;
        }

        public Product? Delete(int id)
        {
            if(id == 0)
            {
                return null;
            }

            Product? product = Products.FirstOrDefault(p => p.Id == id);
            Products.Remove(product);

            return product;
        }

        public Product? GetById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

    }

    
}