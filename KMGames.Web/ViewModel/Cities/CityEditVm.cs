using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.ViewModel.Cities
{
    public class CityEditVm
    {
        //----------PROPERTIES----------//

        public int CityId { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "City's name must be provided")]
        [StringLength(50,ErrorMessage = "City's name must be between {1} and {2} characters",MinimumLength = 1)]
        public string Name { get; set; }

        [DisplayName("Country")]
        [Range(1,int.MaxValue,ErrorMessage = "A country must be selected")]
        public int CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; }

        public byte[] RowVersion { get; set; }
    }
}