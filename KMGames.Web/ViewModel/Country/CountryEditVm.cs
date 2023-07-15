using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Country
{
    public class CountryEditVm
    {
        public int CountryId { get; set; }


        [DisplayName("Country")]
        [Required(ErrorMessage = "{0} must be provided.")]
        [StringLength(50,ErrorMessage = "{0} name must be between {1} and {2} characters.",MinimumLength = 4)]
        public string Name { get; set; }

        public byte[] RowVersion { get; set; }
    }
}