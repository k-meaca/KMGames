using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.Developer;
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
    public class DeveloperRepository : IDeveloperRepository
    {
        //----------PROPERTIES----------//

        private readonly KmGamesDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public DeveloperRepository(KmGamesDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        //----------METHODS----------//
        public void Add(Developer developer)
        {
            _dbContext.Developers.Add(developer);
        }

        public void Delete(Developer developer)
        {
            _dbContext.Developers.Remove(developer);
        }

        public void Edit(Developer developer)
        {
            _dbContext.Entry(developer).State = System.Data.Entity.EntityState.Modified;
        }

        public bool Exist(Developer developer)
        {
            return _dbContext.Developers.Any(d => d.Name == developer.Name && d.DeveloperId != developer.DeveloperId);
        }
        public Developer GetDeveloper(int id)
        {
            return _dbContext.Developers.Include(d => d.Country)
                                        .Include(d => d.City)
                                        .FirstOrDefault(d => d.DeveloperId == id);
        }

        public ICollection<DeveloperListDto> GetDevelopers()
        {
            return _dbContext.Developers.Include("Countries")
                                    .Include("Cities")
                                    .Select(d =>
                                        new DeveloperListDto()
                                        {
                                            DeveloperId = d.DeveloperId,
                                            Name = d.Name,
                                            Country = d.Country.Name,
                                            City = d.City.Name
                                        }
                                    )
                                    .ToList();
        }

        public ICollection<DeveloperListDto> GetDevelopers(int cityId)
        {
            return _dbContext.Developers.Include(d => d.Country)
                                        .Include(d => d.City)
                                        .Where(d => d.CityId == cityId)
                                        .Select(d =>
                                        new DeveloperListDto()
                                        {
                                            DeveloperId = d.DeveloperId,
                                            Name = d.Name,
                                            Country = d.Country.Name,
                                            City = d.City.Name
                                        }
                                        )
                                        .ToList();
        }

        public List<SelectListItem> GetDropDownList()
        {
            return _dbContext.Developers
                        .Select(d =>
                            new SelectListItem()
                            {
                                Text = d.Name,
                                Value = d.DeveloperId.ToString(),
                            }
                        )
                        .ToList();
        }

        public bool ItsRelated(int developerId)
        {
            return _dbContext.Games.Include("Developers")
                            .Any(g => g.DeveloperId == developerId);
        }
    }
}
