using KMGames.Data;
using KMGames.Data.Interfaces;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KMGames.Services.Services
{
    public class CountryService : ICountryService
    {
        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICountryRepository _countryRepository;

        //----------CONSTRUCTOR----------//

        public CountryService(IUnitOfWork unitOfWork, ICountryRepository countryRepository)
        {
            _unitOfWork = unitOfWork;
            _countryRepository = countryRepository;
        }

        //----------METHODS----------//

        public void Add(Country country)
        {
            _countryRepository.Add(country);
            _unitOfWork.SaveChanges();
        }

        public void Delete(Country country)
        {
            _countryRepository.Delete(country);
            _unitOfWork.SaveChanges();
        }

        public void Edit(Country country)
        {
            _countryRepository.Edit(country);
            _unitOfWork.SaveChanges();
        }

        public bool Exist(Country country)
        {
            return _countryRepository.Exist(country);
        }


        public ICollection<Country> GetCountries()
        {
            return _countryRepository.GetCountries();
        }

        public Country GetCountry(int id)
        {
            return _countryRepository.GetCountry(id);
        }

        public List<SelectListItem> GetDropDownList()
        {
            return _countryRepository.GetDropDownList();
        }

        public bool ItsRelated(Country country)
        {
            return _countryRepository.ItsRelated(country);
        }
    }
}
