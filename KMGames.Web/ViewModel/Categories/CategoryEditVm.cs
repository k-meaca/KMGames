using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KMGames.Web.ViewModel.Categories
{
    public class CategoryEditVm
    {
        public int CategoryId { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "The {0} name must be provided.")]
        [StringLength(50,ErrorMessage = "The {0} must be between {2} and {1} characters.",MinimumLength = 3)]
        public string Name { get; set; }

        public byte[] RowVersion { get; set; }

    }
}