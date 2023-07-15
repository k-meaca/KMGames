using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Developers
{
    public class DeveloperSortVm
    {
        public IPagedList<DeveloperListVm> Developers {get;set;}

        [DisplayName("Sort by")]
        public string SortBy { get; set; }

        public Dictionary<string,string> Sorts { get; set; }
    }
}