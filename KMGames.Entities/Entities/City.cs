using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class City
    {
        public int CityId { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public byte[] RowVersion { get; set; }

        public virtual ICollection<Developer> Developers { get; set; }
    }
}
