using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class User
    {
        //----------PROPERTIES----------//

        public int UserId { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public DateTime CreationDate { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
