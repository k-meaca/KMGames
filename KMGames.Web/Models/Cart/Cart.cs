using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMGames.Web.Models.Cart
{
    public class Cart
    {
        //----------PROPERTIES----------//

        public List<ItemCart> Items { get; set; }

        //----------CONSTRUCTOR----------//

        public Cart()
        {
            Items = new List<ItemCart>();
        }

        //----------METHODS----------//

        public int Count()
        {
            return Items.Count;
        } 
    }
}