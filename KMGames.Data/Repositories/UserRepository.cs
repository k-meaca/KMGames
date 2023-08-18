using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.User;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        //----------PROPERTIES----------//

        private readonly KmGamesDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public UserRepository(KmGamesDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //---------METHODS----------//
        public void Add(User user)
        {
            _dbContext.Users.Add(user);
        }

        public void Delete(User user)
        {
            _dbContext.Users.Remove(user);
        }

        public void Edit(User user)
        {
            _dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
        }

        public bool Exist(User user)
        {
            return _dbContext.Users.Any(u => (u.Email == user.Email) && u.UserId != user.UserId);
        }

        public User GetUser(string id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.UserId == id);
        }

        public ICollection<UserListDto> GetUsers()
        {
            return _dbContext.Users.Include(c => c.City)
                                   .Include(c => c.City.Country)
                                   .Select(u => new UserListDto
                                   {
                                        UserId = u.UserId,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                        DNI = u.DNI,
                                        Email = u.Email,
                                        CreationDate = u.CreationDate,
                                        City = u.City.Name,
                                        Country = u.City.Country.Name
                                    })
                                   .ToList();
        }

        public bool ItsRelated(string id)
        {
            return _dbContext.Sales.Any(s => s.UserId == id);
        }
    }
}
