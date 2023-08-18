using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Users
{
    public class UserListVm
    {
        //----------PROPERTIES----------//
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DNI { get; set; }

        public DateTime CreationDate { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

    }
}