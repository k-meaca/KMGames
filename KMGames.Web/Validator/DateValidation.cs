using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KMGames.Web.Validator
{
    public class DateValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) 
            { 
                return false;
            }
            else
            {
                var date = Convert.ToDateTime(value);

                if(date.Year >= 1962 && date.Date <= DateTime.Today.Date)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }            
        }
    }
}