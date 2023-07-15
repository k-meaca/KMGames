using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Users
{
    public class UserListVm
    {
        //----------PROPERTIES----------//
        public int UserId { get; set; }

        public string NickName { get; set; }

        public DateTime CreationDate { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

    }
}