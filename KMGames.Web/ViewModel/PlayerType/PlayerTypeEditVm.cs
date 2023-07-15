using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.PlayerType
{
    public class PlayerTypeEditVm
    {
        //----------PROPERTIES----------//

        public int PlayerTypeId { get; set; }

        [DisplayName("Player Type")]
        [Required(ErrorMessage = "{0} must be provided")]
        [StringLength(100,ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Type { get; set; }

        public byte[] RowVersion { get; set; }
    }
}