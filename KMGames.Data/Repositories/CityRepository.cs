using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.City;
using KMGames.Entities.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KMGames.Data.Repositories
{
    public class CityRepository : ICityRepository
    {
        //----------PROPERTIES----------//

        private readonly KmGamesDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public CityRepository(KmGamesDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        //----------METHODS---------//

        public void Add(City city)
        {
            _dbContext.Cities.Add(city);
        }

        public void Delete(City city)
        {
            _dbContext.Cities.Remove(city);
        }

        public void Edit(City city)
        {
            _dbContext.Entry(city).State = System.Data.Entity.EntityState.Modified;
        }

        public bool Exist(City city)
        {
            return _dbContext.Cities.Any(c => c.Name == city.Name && c.CountryId == city.CountryId);
        }

        public ICollection<CityListDto> GetCities()
        {
            return _dbContext.Cities.Select(c =>
                new CityListDto()
                {
                    CityId = c.CityId,
                    Name = c.Name,
                    CountryId = c.CountryId,
                    Country = c.Country.Name
                }).ToList();
        }

        public ICollection<CityListDto> GetCities(int countryId)
        {
            return _dbContext.Cities.Include(c => c.Country)
                                    .Where(c => c.CountryId == countryId)
                                    .Select(c => new CityListDto()
                                    {
                                        CityId = c.CityId,
                                        Name = c.Name,
                                        CountryId = c.CountryId,
                                        Country = c.Country.Name
                                    })
                                    .ToList();
        }

        public City GetCity(int id)
        {
            return _dbContext.Cities.FirstOrDefault(c => c.CityId == id);
        }

        public List<SelectListItem> GetDropDownListByCountry(int countryId)
        {
            return _dbContext.Cities.Include("Countries")
                                    .Where(c => c.CountryId == countryId)
                                    .Select(c => new SelectListItem()
                                    {
                                        Text = c.Name,
                                        Value = c.CityId.ToString()
                                    })
                                    .ToList();
                                    
        }

        public bool ItsRelated(int id)
        {
            return _dbContext.Developers.Any(d => d.CityId == id);
        }
    }
}
