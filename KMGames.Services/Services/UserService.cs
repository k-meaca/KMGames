using KMGames.Data;
using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.User;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Services.Services
{
    public class UserService : IUserService
    {
        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;

        private readonly IUserRepository _userRepository;

        //----------CONSTRUCTOR----------//

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        
        //----------METHODS----------//

        public void Add(User user)
        {
            _userRepository.Add(user);
            _unitOfWork.SaveChanges();
        }

        public void Delete(User user)
        {
            _userRepository.Delete(user);
            _unitOfWork.SaveChanges();
        }

        public void Edit(User user)
        {
            _userRepository.Edit(user);
            _unitOfWork.SaveChanges();
        }

        public bool Exist(User user)
        {
            return _userRepository.Exist(user);
        }


        public User GetUser(string id)
        {
            return _userRepository.GetUser(id);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public ICollection<UserListDto> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public bool ItsRelated(string id)
        {
            return _userRepository.ItsRelated(id);
        }
    }
}
