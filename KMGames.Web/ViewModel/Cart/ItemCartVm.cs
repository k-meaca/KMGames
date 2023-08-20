using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Cart
{
    public class ItemCartVm
    {
        //----------PROPERTIES----------//

        public int GameId { get; set; }

        public string Title { get; set; }

        [DisplayName("Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ActualPrice { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

    }
}