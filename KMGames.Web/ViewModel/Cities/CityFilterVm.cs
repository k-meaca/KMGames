using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.ViewModel.Cities
{
    public class CityFilterVm
    {
        public List<SelectListItem> Countries { get; set; }

        public IPagedList<CityListVm> Cities { get; set; }

        [DisplayName("Filter by")]
        public int? FilterBy { get; set; }
    }
}