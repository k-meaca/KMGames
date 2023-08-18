using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class User
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address {get;set;}

        public string DNI { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreationDate { get; set; }

        public byte[] RowVersion { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
