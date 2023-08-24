using KMGames.Entities.DTOs.Game;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Data.Interfaces
{
    public interface IGameRepository
    {
        void Add(Game game);
        
        void Delete(Game game);

        void Edit(Game game);

        bool Exist(Game game);

        ICollection<GameListDto> GetBestSeller(int numberOfGames = 3);

        ICollection<GameListDto> GetGames();
        
        List<GameListDto> GetGames(int developerId);

        Game GetGame(int id);

        List<GameListDto> GetGamesByCategory(int? categoryId);
        
        bool ItsRelated(int id);
    }
}
