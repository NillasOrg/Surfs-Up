using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Surfs_Up.Models;

namespace Surfs_Up.Repository
{
    class ShoppingCart
    {
        private readonly List<Surfboard> _items;
        private static ShoppingCart _instance;
        // Singleton :)
        public static ShoppingCart GetInstance()
        {
            if(_instance == null)
            {
                _instance = new ShoppingCart();
            }
            return _instance;
        }

        private ShoppingCart() {
            _items = [];
        }

        public void AddToCart(Surfboard catalogItem) {
            _items.Add(catalogItem);
        }

        public void RemoveFromCart(Surfboard catalogItem) {
            _items.Remove(catalogItem);
        }
        
        public List<Surfboard> GetCartItems() {
            return _items ?? new List<Surfboard>();
        }
    }
    
}