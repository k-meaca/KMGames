using KMGames.Data;
using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.Developer;
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
    public class DeveloperService : IDeveloperService
    {
        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;

        private readonly IDeveloperRepository _developerRepository;

        //----------CONSTRUCTOR----------//

        public DeveloperService(IUnitOfWork unitOfWork, IDeveloperRepository developerRepository)
        {
            _unitOfWork = unitOfWork;
            _developerRepository = developerRepository;
        }

        //----------METHODS----------//
        
        public void Add(Developer developer)
        {
            _developerRepository.Add(developer);
            _unitOfWork.SaveChanges();
        }

        public void Delete(Developer developer)
        {
            _developerRepository.Delete(developer);
            _unitOfWork.SaveChanges();
        }

        public void Edit(Developer developer)
        {
            _developerRepository.Edit(developer);
            _unitOfWork.SaveChanges();
        }

        public bool Exist(Developer developer)
        {
            return _developerRepository.Exist(developer);
        }

        public Developer GetDeveloper(int id)
        {
            return _developerRepository.GetDeveloper(id);
        }

        public ICollection<DeveloperListDto> GetDevelopers()
        {
            return _developerRepository.GetDevelopers();
        }

        public ICollection<DeveloperListDto> GetDevelopers(int cityId)
        {
            return _developerRepository.GetDevelopers(cityId);
        }

        public List<SelectListItem> GetDropDownList()
        {
            return _developerRepository.GetDropDownList();
        }

        public bool ItsRelated(int developerId)
        {
            return _developerRepository.ItsRelated(developerId);
        }
    }
}
