using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Surfs_Up.Models;

namespace Surfs_Up.Repository
{
    class ShoppingCart
    {
        private readonly List<CatalogItem> _items;
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

        public void AddToCart(CatalogItem catalogItem) {
            _items.Add(catalogItem);
        }

        public void RemoveFromCart(CatalogItem catalogItem) {
            _items.Remove(catalogItem);
        }
        
        public List<CatalogItem> GetCartItems() {
            return _items ?? new List<CatalogItem>();
        }
    }
    
}