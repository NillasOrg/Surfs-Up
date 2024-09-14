using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Surfs_Up.Models;

namespace Surfs_Up.Repository
{
    class ShoppingCart
    {
        private readonly List<CatalogItem> items;
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
            items = [];
        }

        public void AddToCart(CatalogItem catalogItem) {
            items.Add(catalogItem);
        }

        public void RemoveFromCart(CatalogItem catalogItem) {
            items.Remove(catalogItem);
        }
        
        public List<CatalogItem> GetCartItems() {
            return items;
        }
    }
    
}