using KMGames.Web.ViewModel.Categories;
using KMGames.Web.ViewModel.Games;
using KMGames.Web.ViewModel.PlayerType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Buyables
{
    public class BuyableDetailsVm
    {
        public GameListVm Game { get; set; }

        public List<CategoryListVm> Categories { get; set; }

        [DisplayName("Players")]
        public List<PlayerTypeListVm> PlayerTypes { get; set; }

        public string Filter { get; set; }
    }
}