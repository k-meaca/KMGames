using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.Game;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Html;

namespace KMGames.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        //----------PROPERTIES----------//

        private readonly KmGamesDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public GameRepository(KmGamesDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //----------METHODS----------//

        public void Add(Game game)
        {
            _dbContext.Games.Add(game);
        }

        public void Delete(Game game)
        {
            _dbContext.Games.Remove(game);
        }

        public void Edit(Game game)
        {
            _dbContext.Entry(game).State = System.Data.Entity.EntityState.Modified;
        }

        public bool Exist(Game game)
        {
            return _dbContext.Games.Any(g => g.Title == game.Title && g.GameId != game.GameId);
        }

        public ICollection<GameListDto> GetBestSeller(int numberOfGames = 3)
        {

            var topIds = _dbContext.SalesDetais.Include(sd => sd.Game)
                                         .GroupBy(sd => sd.Game, sd => sd.GameId)
                                         .Select(sd => new { Id = sd.Key.GameId, Total = sd.Count() })
                                         .OrderByDescending(g => g.Total)
                                         .Take(numberOfGames)
                                         .ToList();
            
            var bestSeller = new List<GameListDto>();

            topIds.ForEach(id =>
                {
                    var game = _dbContext.Games.Include(g => g.Developer).FirstOrDefault(g => g.GameId == id.Id);

                    bestSeller.Add(new GameListDto()
                    {
                        GameId = game.GameId,
                        Title = game.Title,
                        ActualPrice = game.ActualPrice,
                        Release = game.Release,
                        Developer = game.Developer.Name,
                        Image = game.Image
                    });
                }
            );

            return bestSeller;
        }

        public Game GetGame(int id)
        {
            Game game = _dbContext.Games.Include(g => g.Developer)
                                        .Include(g => g.GameCategories)
                                        .Include(g => g.PlayersGame)
                                        .FirstOrDefault(g => g.GameId == id);

            return game;
        }

        public ICollection<GameListDto> GetGames()
        {
            return _dbContext.Games.Include(g => g.Developer)
                                    .Include(g => g.GameCategories)
                                   .Include(g => g.PlayersGame)
                                    .Select(g => new GameListDto
                                    {
                                        GameId = g.GameId,
                                        Title = g.Title,
                                        ActualPrice = g.ActualPrice,
                                        Release = g.Release,
                                        Developer = g.Developer.Name,
                                        Image = g.Image
                                    })
                                    .ToList();
        }

        public List<GameListDto> GetGames(int developerId)
        {
            return _dbContext.Games.Include(g => g.Developer)
                                    .Where(g => g.DeveloperId == developerId)
                                    .Select(g => new GameListDto
                                    {
                                        GameId = g.GameId,
                                        Title = g.Title,
                                        ActualPrice = g.ActualPrice,
                                        Release = g.Release,
                                        Developer = g.Developer.Name,
                                        Image = g.Image
                                    })
                                    .ToList();
        }

        public List<GameListDto> GetGamesByCategory(int? categoryId)
        {
            List<GameListDto> games;

            if(categoryId is null)
            {
                games = GetGames().ToList();
            }
            else
            {
                games = _dbContext.Games.Include(g => g.GameCategories)
                                        .Where(g => g.GameCategories.Any(gc => gc.CategoryId == categoryId.Value))
                                        .Select(g => new GameListDto
                                        {
                                            GameId = g.GameId,
                                            Title = g.Title,
                                            ActualPrice = g.ActualPrice,
                                            Release = g.Release,
                                            Developer = g.Developer.Name,
                                            Image = g.Image
                                        })
                                        .ToList();
            }

            return games;
        }

        public bool ItsRelated(int id)
        {
            return _dbContext.SalesDetais.Any(sd => sd.GameId == id);
        }
    }
}
