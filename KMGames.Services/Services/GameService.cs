using KMGames.Data;
using KMGames.Data.Interfaces;
using KMGames.Entities;
using KMGames.Entities.DTOs.Game;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using System;
using System.Transactions;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Services.Services
{
    public class GameService : IGameService
    {

        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameRepository _gameRepository;


        //----------CONSTRUCTOR----------//

        public GameService(IUnitOfWork unitOfWork, IGameRepository gameRepository)
        {
            _unitOfWork = unitOfWork;
            _gameRepository = gameRepository;
        }

        //----------METHODS----------//

        public void Add(Game game)
        {
            try
            {
                using(var transaction = new TransactionScope())
                {
                    _gameRepository.Add(game);
                    _unitOfWork.SaveChanges();

                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Game game)
        {
            _gameRepository.Delete(game);
            _unitOfWork.SaveChanges();
        }

        public void Edit(Game gameEdit)
        {
            try
            {
                var game = _gameRepository.GetGame(gameEdit.GameId);

                game.ActualPrice = gameEdit.ActualPrice;
                game.Title = gameEdit.Title;
                game.Release = gameEdit.Release;
                game.DeveloperId = gameEdit.DeveloperId;
                game.GameCategories = gameEdit.GameCategories;
                game.PlayersGame = gameEdit.PlayersGame;
                game.Image = gameEdit.Image;

                _gameRepository.Edit(game);
                _unitOfWork.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Exist(Game game)
        {
            return _gameRepository.Exist(game);
        }

        public ICollection<GameListDto> GetBestSeller(int numberOfGames = 3)
        {
            return _gameRepository.GetBestSeller(numberOfGames);
        }

        public Game GetGame(int id)
        {
            return _gameRepository.GetGame(id);
        }

        public ICollection<GameListDto> GetGames()
        {
            return _gameRepository.GetGames();
        }

        public List<GameListDto> GetGames(int developerId)
        {
            return _gameRepository.GetGames(developerId);
        }

        public List<GameListDto> GetGamesByCategory(int categoryId)
        {
            return _gameRepository.GetGamesByCategory(categoryId);
        }

        public bool ItsRelated(int id)
        {
            return _gameRepository.ItsRelated(id);
        }
    }
}
