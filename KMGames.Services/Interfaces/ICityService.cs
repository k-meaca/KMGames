using KMGames.Entities.DTOs.City;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KMGames.Services.Interfaces
{
    public interface ICityService
    {
        void Add(City city);

        void Delete(City city);

        void Edit(City city);

        bool Exist(City city);

        City GetCity(int id);

        ICollection<CityListDto> GetCities();

        ICollection<CityListDto> GetCities(int countryId);

        List<SelectListItem> GetDropDownListByCountry(int countryId);

        bool ItsRelated(int value);
    }
}
