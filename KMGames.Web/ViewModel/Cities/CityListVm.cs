using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Cities
{
    public class CityListVm
    {
        //----------PROPERTIES----------//

        public int CityId { get; set; }

        [DisplayName("City")]
        public string Name { get; set; }

        [DisplayName("Country")]
        public int CountryId { get; set; }
        public string Country { get; set; }
    }
}