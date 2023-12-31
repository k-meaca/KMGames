﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Entities.DTOs.Sale
{
    public class SaleListDto
    {
        //----------PROPERTIES----------//

        public int SaleId { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public decimal Total { get; set; }
    }
}
