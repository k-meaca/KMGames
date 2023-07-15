using KMGames.Entities.DTOs.User;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Data.Interfaces
{
    public interface IUserRepository
    {

        void Add(User user);

        void Delete(User user);

        void Edit(User user);

        bool Exist(User user);

        ICollection<UserListDto> GetUsers();

        User GetUser(int id);

        bool ItsRelated(int id);
    }
}
