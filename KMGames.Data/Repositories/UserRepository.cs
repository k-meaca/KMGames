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
            return _dbContext.Users.Any(u => (u.NickName == user.NickName || u.Email == user.Email) && u.UserId != user.UserId);
        }

        public User GetUser(int id)
        {
            return _dbContext.Users.Include(c => c.Country).FirstOrDefault(u => u.UserId == id);
        }

        public ICollection<UserListDto> GetUsers()
        {
            return _dbContext.Users.Include(c => c.Country)
                        .Select(u => new UserListDto
                        {
                            UserId = u.UserId,
                            NickName = u.NickName,
                            CreationDate = u.CreationDate,
                            Country = u.Country.Name,
                            Email = u.Email
                        })
                        .ToList();

        }

        public bool ItsRelated(int id)
        {
            return _dbContext.Sales.Any(s => s.UserId == id);
        }
    }
}
