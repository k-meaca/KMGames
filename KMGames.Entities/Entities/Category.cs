using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class Category
    {
        //----------PROPERTIES----------//

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public byte[] RowVersion { get; set; }

        public virtual ICollection<GameCategory> GameCategories { get; set; }

        //----------CONSTRUCTOR----------//

        public Category()
        {
            this.GameCategories = new HashSet<GameCategory>();
        }

    }
}
