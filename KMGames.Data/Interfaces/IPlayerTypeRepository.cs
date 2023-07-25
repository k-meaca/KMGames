using KMGames.Entities.DTOs.PlayerType;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KMGames.Data.Interfaces
{
    public interface IPlayerTypeRepository
    {
        void Add(PlayerType playerType);

        void Delete(PlayerType playerType);

        void Edit(PlayerType playerType);

        bool Exist(PlayerType playerType);

        ICollection<PlayerTypeListDto> GetPlayerTypes();

        List<PlayerTypeListDto> GetPlayerTypesFrom(int gameId);

        PlayerType GetPlayerType(int id);

        List<PlayerTypeCheckDto> GetCheckBoxList();

        bool ItsRelated(int id);
    }
}
