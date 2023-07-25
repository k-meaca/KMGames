using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMGames.Web.Models.Cart
{
    public class ItemCart
    {
        //------------PROPERTIES-----------//

        public int GameId { get; set; }

        public Game Game { get; set; }

        public decimal SalePrice { get; set; }
    }
}