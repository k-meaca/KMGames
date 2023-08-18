using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.DTOs.User
{
    public class UserListDto
    {
        //----------PROPERTIES----------//

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DNI { get; set; }

        public string Email { get; set; }

        public DateTime CreationDate { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        //----------CONSTRUCTOR----------//
    }
}
