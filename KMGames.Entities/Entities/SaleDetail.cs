using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class SaleDetail
    {
        //----------PROPERTIES----------//

        public int SaleId { get; set; }
        
        public int GameId { get; set; }

        public virtual Sale Sale { get; set; }

        public virtual Game Game { get; set; }

        public decimal SalePrice { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
