using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.PlayerType
{
    public class PlayerTypeCheckVm
    {
        //----------PROPERTIES----------//

        public int PlayerTypeId { get; set; }

        public string Type { get; set; }

        public bool Selected { get; set; }
    }
}