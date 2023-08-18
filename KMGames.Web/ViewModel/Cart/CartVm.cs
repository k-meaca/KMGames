using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Cart
{
    public class CartVm
    {
        public List<ItemCartVm> Items { get; set; }

        public string LastCategory { get; set; }
    }
}