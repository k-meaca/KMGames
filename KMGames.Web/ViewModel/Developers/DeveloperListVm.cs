using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Developers
{
    public class DeveloperListVm
    {
        //----------PROPERTIES---------//

        public int DeveloperId { get; set; }


        [DisplayName("Developer")]
        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }
    }
}