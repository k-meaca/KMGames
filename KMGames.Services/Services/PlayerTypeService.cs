using KMGames.Data;
using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.PlayerType;
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
    public class PlayerTypeService : IPLayerTypeService
    {
        //----------PROPERTIES----------//

        private readonly IPlayerTypeRepository _playerTypeRepository;

        private readonly IUnitOfWork _unitOfWork;

        //----------CONSTRUCTOR----------//
        public PlayerTypeService(IUnitOfWork unitOfWork, IPlayerTypeRepository playerTypeRepository)
        {
            _playerTypeRepository = playerTypeRepository;

            _unitOfWork = unitOfWork;
        }

        //----------METHODS----------//

        public void Add(PlayerType playerType)
        {
            _playerTypeRepository.Add(playerType);
            _unitOfWork.SaveChanges();
        }

        public void Delete(PlayerType playerType)
        {
            _playerTypeRepository.Delete(playerType);
            _unitOfWork.SaveChanges();
        }

        public void Edit(PlayerType playerType)
        {
            _playerTypeRepository.Edit(playerType);
            _unitOfWork.SaveChanges();
        }

        public bool Exist(PlayerType playerType)
        {
            return _playerTypeRepository.Exist(playerType);
        }

        public ICollection<PlayerTypeListDto> GetPlayerTypes()
        {
            return _playerTypeRepository.GetPlayerTypes();
        }

        public List<PlayerTypeCheckDto> GetCheckBoxList()
        {
            return _playerTypeRepository.GetCheckBoxList();
        }

        public PlayerType GetPlayerType(int id)
        {
            return _playerTypeRepository.GetPlayerType(id);
        }

        public bool ItsRelated(int id)
        {
            return _playerTypeRepository.ItsRelated(id);
        }
    }
}
