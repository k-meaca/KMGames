using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Sales
{
    public class CustomerSalesListVm
    {
        public string UserId { get; set; }

        [DisplayName("User Email")]
        public string UserEmail { get; set; }

        [DisplayName("Full Name")]
        public string UserName { get; set; }

        [DisplayName("Spent")]
        public decimal TotalSpent { get; set; }

        [DisplayName("Purchased Games")]
        public int PurchasedGames { get; set; }

    }
}