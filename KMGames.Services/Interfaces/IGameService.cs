using KMGames.Entities;
using KMGames.Entities.DTOs.Game;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Services.Interfaces
{
    public interface IGameService
    {
        void Add(Game game);

        void Delete(Game game);

        void Edit(Game game);

        bool Exist(Game game);

        ICollection<GameListDto> GetGames();
        
        List<GameListDto> GetGames(int developerId);

        Game GetGame(int id);

        bool ItsRelated(int id);
    }
}
