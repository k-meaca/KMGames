using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMGames.Web.Models.SessionCart
{
    public class SessionCart
    {
        //----------PROPERTIES----------//

        public List<ItemCart> Items { get; set; }

        public string LastCategory { get; set; }

        //----------CONSTRUCTOR----------//

        public SessionCart()
        {
            Items = new List<ItemCart>();
        }

        //----------METHODS----------//

        public int Count()
        {
            return Items.Count;
        } 

        public void AddGame(ItemCart game)
        {
            Items.Add(game);
        }

        public void Clear()
        {
            Items.Clear();
        }

        
        public void RemoveGame(int id)
        {
            Items.Remove(Items.First(i => i.GameId == id));
        }
    }
}