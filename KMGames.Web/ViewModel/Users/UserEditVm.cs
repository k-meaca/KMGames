using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.ViewModel.Users
{
    public class UserEditVm     
    {
        public string UserId { get; set; }

        [DisplayName("First Name")]
        [StringLength(256,ErrorMessage = "User name must be between {2} and {1} characters.",MinimumLength = 1)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(256, ErrorMessage = "User name must be between {2} and {1} characters.", MinimumLength = 1)]
        public string LastName { get; set; }

        [StringValidator(InvalidCharacters = ".:,;<>´¨+*~{}[] -_|¬°!\"#$%&/()=?'¡¿\t\nabcdefghijklmnñopqrstuvwxyz")]
        [StringLength(25,ErrorMessage = "DNI must be between {2} and {1} numbers without dots (.)",MinimumLength = 7)]
        public string DNI { get; set; }

        [Required(ErrorMessage = "Mail must be entered.")]
        [StringLength(int.MaxValue,ErrorMessage = "Email must have more than {2} characters.",MinimumLength = 11)]
        public string Email { get; set; }

        public string DeprecatedEmail { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Account creation date")]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "City must be selected.")]
        [Range(1, int.MaxValue)]
        public int CityId { get; set; }

        public List<SelectListItem> Cities { get; set; }

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