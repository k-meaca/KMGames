using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.ViewModel.Developers
{
    public class DeveloperEditVm
    {
        public int DeveloperId { get; set; }

        [DisplayName("Developer")]
        [Required(ErrorMessage = "Developers name must be provided")]
        [StringLength(100,ErrorMessage = "The name must be between {1} and {2} characters.",MinimumLength = 2)]
        public string Name { get; set; }

        public byte[] RowVersion { get; set; }

        [DisplayName("Country")]
        [Range(1,int.MaxValue,ErrorMessage = "Country must be selected.")]
        public int CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; }

        [DisplayName("City")]
        [Range(1, int.MaxValue, ErrorMessage = "City must be selected.")]
        public int CityId { get; set; }

        public List<SelectListItem> Cities { get; set; }
    }
}