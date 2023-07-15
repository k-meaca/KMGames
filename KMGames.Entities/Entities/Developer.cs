using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class Developer
    {
        //----------PROPERTIES----------//

        public int DeveloperId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public byte[] RowVersion { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        //----------CONSTRUCTOR----------//

        public Developer()
        {
            this.Games = new HashSet<Game>();
        }

    }
}
