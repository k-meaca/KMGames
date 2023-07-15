using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Categories
{
    public class CategoryCheckVm
    {
        //-----------PROPERTIES----------//
        public int CategoryId { get; set; }

        public string Category { get; set; }

        public bool Selected { get; set; }
    }
}