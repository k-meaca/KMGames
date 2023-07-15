using KMGames.Web.ViewModel.Categories;
using KMGames.Web.ViewModel.PlayerType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KMGames.Web.Validator
{
    public class OneItemSelectedValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            if(value is List<CategoryCheckVm>)
            {
                var list = value as List<CategoryCheckVm>;

                return list.Any(c => c.Selected == true);
            }
            else
            {
                var list = value as List<PlayerTypeCheckVm>;

                return list.Any(c => c.Selected == true);
            }
        }
    }
}