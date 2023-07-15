using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KMGames.Services.Interfaces
{
    public interface ICountryService
    {
        void Add(Country country);

        void Delete(Country country);

        void Edit(Country country);

        bool Exist(Country country);
        
        ICollection<Country> GetCountries();

        Country GetCountry(int id);

        List<SelectListItem> GetDropDownList();

        bool ItsRelated(Country country);

    }
}
