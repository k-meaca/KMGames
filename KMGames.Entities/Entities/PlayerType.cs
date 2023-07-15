using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class PlayerType
    {
        //----------PROPERTIES----------//

        public int PlayerTypeId { get; set; }

        public string Type { get; set; }

        public byte[] RowVersion { get; set; }

        public virtual ICollection<PlayerGame> PlayersGames { get; set; }

        //----------CONSTRUCTOR----------//

        public PlayerType()
        {
            this.PlayersGames = new HashSet<PlayerGame>();
        }
    }
}
