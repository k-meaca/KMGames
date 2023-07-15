using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.ViewModel.Users
{
    public class UserEditVm     
    {
        public int UserId { get; set; }

        [DisplayName("User")]
        [StringLength(100,ErrorMessage = "User name must be between {2} and {1} characters.",MinimumLength = 4)]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Mail must be entered.")]
        [StringLength(int.MaxValue,ErrorMessage = "Email must have more than {2} characters.",MinimumLength = 17)]
        public string Email { get; set; }

        public string DeprecatedEmail { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "Country must be selected.")]
        [Range(1,int.MaxValue)]
        public int CountryId { get; set; }

        public List<SelectListItem> Countries { get; set; }

        [StringLength(int.MaxValue,ErrorMessage = "Address must be grater than {2} characters.",MinimumLength = 10)]
        public string Address { get; set; }

        public byte[] RowVersion { get; set; }
    }
}