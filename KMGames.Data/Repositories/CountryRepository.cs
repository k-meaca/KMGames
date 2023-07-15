using KMGames.Data.Interfaces;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace KMGames.Data.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        //----------PROPERTIES----------//

        private readonly KmGamesDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public CountryRepository(KmGamesDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        //----------METHODS----------//

        public void Add(Country country)
        {
            _dbContext.Countries.Add(country);
        }

        public void Delete(Country country)
        {
            _dbContext.Countries.Remove(country);
        }

        public void Edit(Country country)
        {
            _dbContext.Entry(country).State = System.Data.Entity.EntityState.Modified;
        }

        public bool Exist(Country country)
        {
            return _dbContext.Countries.Any(c => c.Name == country.Name);
        }

        public ICollection<Country> GetCountries()
        {
            return _dbContext.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return _dbContext.Countries.FirstOrDefault(c => c.CountryId == id);
        }

        public List<SelectListItem> GetDropDownList()
        {
            return _dbContext.Countries.Select(c => new SelectListItem()
                                    {
                                        Text = c.Name,
                                        Value = c.CountryId.ToString()
                                    })
                                    .ToList();
        }

        public bool ItsRelated(Country country)
        {
            return _dbContext.Developers.Any(d => d.CountryId == country.CountryId);
        }
    }
}
