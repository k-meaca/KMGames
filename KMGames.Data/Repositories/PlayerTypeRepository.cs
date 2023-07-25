using KMGames.Data.Interfaces;
using KMGames.Entities.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using KMGames.Entities.DTOs.PlayerType;

namespace KMGames.Data.Repositories
{
    public class PlayerTypeRepository : IPlayerTypeRepository
    {
        //----------PROPERTIES----------//

        private readonly KmGamesDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public PlayerTypeRepository(KmGamesDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        //----------METHODS----------//
        public void Add(PlayerType playerType)
        {
            _dbContext.PlayerTypes.Add(playerType);
        }

        public void Delete(PlayerType playerType)
        {
            _dbContext.PlayerTypes.Remove(playerType);
        }

        public void Edit(PlayerType playerType)
        {
            _dbContext.Entry(playerType).State = System.Data.Entity.EntityState.Modified;
        }

        public bool Exist(PlayerType playerType)
        {
            return _dbContext.PlayerTypes.Any(p => p.Type == playerType.Type);
        }

        public List<PlayerTypeCheckDto> GetCheckBoxList()
        {
            return _dbContext.PlayerTypes.Select(p => new PlayerTypeCheckDto()
            {
                PlayerTypeId = p.PlayerTypeId,
                Type = p.Type,
                Selected = false
            }).ToList();
        }

        public PlayerType GetPlayerType(int id)
        {
            return _dbContext.PlayerTypes.FirstOrDefault(p => p.PlayerTypeId == id);
        }

        public ICollection<PlayerTypeListDto> GetPlayerTypes()
        {
            return _dbContext.PlayerTypes.Select(p => new PlayerTypeListDto()
            {
                PlayerTypeId = p.PlayerTypeId,
                Type = p.Type
            }).ToList();
        }

        public List<PlayerTypeListDto> GetPlayerTypesFrom(int gameId)
        {
            return _dbContext.PlayersGames.Include(pg => pg.PlayerType)
                                          .Where(pg => pg.GameId == gameId)
                                          .Select(pg => new PlayerTypeListDto()
                                          {
                                              PlayerTypeId = pg.PlayerTypeId,
                                              Type = pg.PlayerType.Type
                                          })
                                          .ToList();
        }

        public bool ItsRelated(int id)
        {
            return _dbContext.PlayersGames.Any(pg => pg.PlayerTypeId == id);
        }
    }
}
