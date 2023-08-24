using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.DTOs.Sale
{
    public class CustomerSalesListDto
    {
        public string UserId { get; set; }

        public List<int> UserSales { get; set; }

        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public decimal TotalSpent { get; set; }

        public int PurchasedGames { get; set; }

        //----------CONSTRUCTOR----------//

        public CustomerSalesListDto()
        {
            UserSales = new List<int>();
        }
    }
}
