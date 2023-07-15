using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class PlayerGame
    {
        //----------PROPERTIES----------//

        public int GameId { get; set; }

        public int PlayerTypeId { get; set; }

        public virtual Game Game { get; set; }

        public virtual PlayerType PlayerType { get; set; }

        public byte[] RowVersion { get; set; }

    }
}
