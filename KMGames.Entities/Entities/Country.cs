using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class Country
    {
        //----------PROPERTIES----------//

        public int CountryId { get; set; }

        public string Name { get; set; }

        public byte[] RowVersion { get; set; }

        public virtual ICollection<City> Cities {get;set;}

        public virtual ICollection<Developer> Developers { get; set; }

        public virtual ICollection<int> Users { get; set; }
    }
}
