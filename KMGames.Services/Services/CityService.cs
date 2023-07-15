using KMGames.Data;
using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.City;
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
    public class CityService : ICityService
    {
        //----------PROPERTIES----------//

        private readonly ICityRepository _cityRepository;

        private readonly IUnitOfWork _unitOfWork;

        //----------CONSTRUCTOR----------//

        public CityService(IUnitOfWork unitOfWork, ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        //----------METHODS----------//

        public void Add(City city)
        {
            _cityRepository.Add(city);
            _unitOfWork.SaveChanges();
        }

        public void Delete(City city)
        {
            _cityRepository.Delete(city);
            _unitOfWork.SaveChanges();
        }

        public void Edit(City city)
        {
            _cityRepository.Edit(city);
            _unitOfWork.SaveChanges();
        }

        public bool Exist(City city)
        {
            return _cityRepository.Exist(city);
        }

        public ICollection<CityListDto> GetCities()
        {
            return _cityRepository.GetCities();
        }

        public ICollection<CityListDto> GetCities(int countryId)
        {
            return _cityRepository.GetCities(countryId);
        }

        public City GetCity(int id)
        {
            return _cityRepository.GetCity(id);
        }

        public List<SelectListItem> GetDropDownListByCountry(int countryId)
        {
            return _cityRepository.GetDropDownListByCountry(countryId);
        }

        public bool ItsRelated(int id)
        {
            return _cityRepository.ItsRelated(id);
        }
    }
}
