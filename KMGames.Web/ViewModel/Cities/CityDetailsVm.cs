using KMGames.Web.ViewModel.Developers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Cities
{
    public class CityDetailsVm
    {
        public CityListVm City { get; set; }

        public List<DeveloperListVm> Developers { get; set; }
    }
}