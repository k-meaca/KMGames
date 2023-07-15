using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Categories
{
    public class CategoryListVm
    {
        public int CategoryId { get; set; }

        [DisplayName("Category")]
        public string Name { get; set; }

    }
}