using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Surfs_Up.Models;

namespace Surfs_Up.Repository
{
    class ShoppingCart
    {
        private readonly List<ICartItem> cart;
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

        public ShoppingCart()
        {
            cart = new List<ICartItem>();
        }

        public void AddToCart<T>(T item) where T : ICartItem
        {
            cart.Add(item);
        }

        public void RemoveFromCart<T>(T item) where T : ICartItem 
        {
            cart.Remove(item);
        }
        
        public List<T> GetItemsOfType<T>() where T : ICartItem
        {
            return cart.OfType<T>().ToList();
        }

    }
    
}