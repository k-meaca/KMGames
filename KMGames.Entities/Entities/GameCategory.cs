using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class GameCategory
    {
        //---------PROPERTIES----------//

        public int GameId { get; set; }

        public int CategoryId { get; set; }

        public virtual Game Game { get; set; }

        public virtual Category Category { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
