using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Games
{
    public class GameListVm
    {
        //----------PROPERTIES----------//

        public int GameId { get; set; }

        [DisplayName("Game")]
        public string Title { get; set; }

        [DisplayName("Price")]

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ActualPrice { get; set; }

        [DataType(DataType.Date)]
        public DateTime Release { get; set; }

        public string Developer { get; set; }

        public byte[] RowVersion { get; set; }
    }
}