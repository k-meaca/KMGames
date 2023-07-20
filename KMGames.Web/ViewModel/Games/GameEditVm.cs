using KMGames.Entities.DTOs.PlayerType;
using KMGames.Entities.Entities;
using KMGames.Web.Validator;
using KMGames.Web.ViewModel.Categories;
using KMGames.Web.ViewModel.PlayerType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace KMGames.Web.ViewModel.Games
{
    public class GameEditVm
    {
        //----------PROPERTIES----------//

        public int GameId { get; set; }

        [DisplayName("Game")]
        [Required(ErrorMessage = "{0} can't be empty.")]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters.", MinimumLength = 3)]
        public string Title { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "{0} can't be empty.")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(1.0d,100.0d,ErrorMessage = "{0} must be between {1:C} and {2:C}")]
        public decimal ActualPrice { get; set; }

        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Date can't be later than today or previous than 1962.")]
        public DateTime Release { get; set; }

        public byte[] RowVersion { get; set; }

        [DisplayName("Developer")]
        [Required(ErrorMessage = "Developer must be selected.")]
        [Range(1,int.MaxValue)]
        public int DeveloperId { get; set; }

        public List<SelectListItem> Developers { get; set; }

        [DisplayName("Categories")]
        [OneItemSelectedValidation(ErrorMessage = "You must peek at least one category.")]
        public List<CategoryCheckVm> Categories { get; set; }


        [DisplayName("Player types")]
        [OneItemSelectedValidation(ErrorMessage = "You must peek at least one player type.")]
        public List<PlayerTypeCheckVm> PlayerTypes { get; set; }

        public  ICollection<GameCategory> GameCategories { get; set; }

        public  ICollection<PlayerGame> PlayersGame { get; set; }

        public  ICollection<SaleDetail> SalesDetails { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [DisplayName("Image")]
        public HttpPostedFileBase imageFile { get; set; }

    }
}