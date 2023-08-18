using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.Entities
{
    public class Sale
    {
        //----------PROPERTIES----------//

        public int SaleId { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<SaleDetail> SalesDetails { get; set; }

        public decimal Total { get; set; }

        public byte[] RowVersion { get; set; }

        //----------CONSTRUCTOR----------//

        public Sale()
        {
            SalesDetails = new HashSet<SaleDetail>();
        }
    }
}
