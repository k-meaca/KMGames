using KMGames.Web.ViewModel.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Country
{
    public class CountryDetailsVm
    {
        public CountryListVm Country { get; set; }

        public List<CityListVm> Cities { get; set; }
    }
}