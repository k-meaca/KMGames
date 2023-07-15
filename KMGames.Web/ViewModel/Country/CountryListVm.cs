using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Country
{
    public class CountryListVm
    {
        public int CountryId { get; set; }


        [DisplayName("Country")]
        public string Name { get; set; }
    }
}