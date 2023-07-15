using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.PlayerType
{
    public class PlayerTypeListVm
    {
        //----------PROPERTIES----------//

        public int PlayerTypeId { get; set; }

        [DisplayName("Player Type")]
        public string Type { get; set; }
    }
}