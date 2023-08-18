using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMGames.Web.Models.Cart
{
    public class Cart
    {
        //----------PROPERTIES----------//

        private List<ItemCart> _items { get; set; }

        public string LastCategory { get; set; }

        //----------CONSTRUCTOR----------//

        public Cart()
        {
            _items = new List<ItemCart>();
        }

        //----------METHODS----------//

        public int Count()
        {
            return _items.Count;
        } 

        public void AddGame(ItemCart game)
        {
            _items.Add(game);
        }

        public List<ItemCart> Items()
        {
            return _items;
        }

        public void RemoveGame(int id)
        {
            _items.Remove(_items.First(i => i.GameId == id));
        }
    }
}