using KMGames.Entities.Entities;
using KMGames.Web.ViewModel.Categories;
using KMGames.Web.ViewModel.PlayerType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Games
{
    public class BuyableGameListVm
    {
        public List<GameListVm> Games { get; set; }

        public List<CategoryListVm> Categories {get; set;}

        public List<PlayerTypeListVm> PlayersGame { get; set; }

        public int FilterId { get; set; }

        public Category Filter { get; set; }
    }
}