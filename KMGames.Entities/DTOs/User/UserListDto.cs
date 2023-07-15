using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.DTOs.User
{
    public class UserListDto
    {
        //----------PROPERTIES----------//

        public int UserId { get; set; }

        public string NickName { get; set; }

        public DateTime CreationDate { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        //----------CONSTRUCTOR----------//
    }
}
