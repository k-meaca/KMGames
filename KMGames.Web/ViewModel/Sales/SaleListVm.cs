using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Sales
{
    public class SaleListVm
    {
        //----------PROPERTIES----------//

        [DisplayName("Sale")]
        public int SaleId { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        [DisplayName("User Email")]
        public string UserEmail { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Total { get; set; }
    }
}