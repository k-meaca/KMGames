using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class Cart
    {
        //----------PROPERTIES-----------//
        public int CartId { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<GameInCart> Games { get; set; }

        public byte[] RowVersion { get; set; }

        //----------CONSTRUCTOR----------//

        public Cart()
        {
            Games = new HashSet<GameInCart>();
        }

    }
}
