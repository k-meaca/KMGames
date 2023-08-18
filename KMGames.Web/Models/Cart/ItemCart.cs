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

        public string Title { get; set; }

        public decimal SalePrice { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        //----------CONSTRUCTOR----------//

        public ItemCart(Game game)
        {
            GameId = game.GameId;
            Title = game.Title;
            SalePrice = game.ActualPrice;
            Image = game.Image;
            Description = game.Description;
        }
    }
}