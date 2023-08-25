using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class GameInCart
    {
        //----------PROPERTIES----------//

        public int CartId { get; set; }

        public virtual Cart Cart {get;set;}

        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
