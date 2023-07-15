using KMGames.Entities.DTOs.Developer;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KMGames.Data.Interfaces
{
    public interface IDeveloperRepository
    {
        void Add(Developer developer);
        
        void Delete(Developer developer);
        
        void Edit(Developer developer);

        bool Exist(Developer developer);

        ICollection<DeveloperListDto> GetDevelopers();
        ICollection<DeveloperListDto> GetDevelopers(int cityId);

        Developer GetDeveloper(int id);

        List<SelectListItem> GetDropDownList();
        
        bool ItsRelated(int developerId);
    }
}
