using AutoMapper;
using KMGames.Entities.DTOs.Game;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using KMGames.Web.App_Start;
using KMGames.Web.ViewModel.Buyables;
using KMGames.Web.ViewModel.Categories;
using KMGames.Web.ViewModel.Games;
using KMGames.Web.ViewModel.PlayerType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.Controllers
{
    public class BuyableController : Controller
    {
        //----------PROPERTIES----------//

        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;
        private readonly IPLayerTypeService _typesService;


        private readonly IMapper _mapper;

        //----------CONTRUCTOR----------//

        public BuyableController(IGameService gameService, ICategoryService categoryService, IPLayerTypeService playerTypes)
        {
            _gameService = gameService;
            _categoryService = categoryService;
            _typesService = playerTypes;

            _mapper = AutoMapperConfig.Mapper;
        }

        // GET: Buyable
        public ActionResult Index(string filter = null)
        {

            List<GameListDto> games;
            Category category;
            List<GameListVm> gamesListVm;

            category = _categoryService.GetCategory(filter);

            if(category is null)
            {
                games = _gameService.GetGames().ToList();
            }
            else
            {
                games = _gameService.GetGamesByCategory(category.CategoryId);
            }

            gamesListVm = _mapper.Map<List<GameListVm>>(games);

            BuyableGameListVm buyableGames = new BuyableGameListVm
            {
                Games = gamesListVm,
                Categories = _mapper.Map<List<CategoryListVm>>(_categoryService.GetCategories()),
                PlayersGame = _mapper.Map<List<PlayerTypeListVm>>(_typesService.GetPlayerTypes()),
                Filter = category
            };

            return View(buyableGames);
        }

        public ActionResult Details(int? gameId, string filter)
        {
            if(gameId is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var game = _gameService.GetGame(gameId.Value);

            if(game is null)
            {
                TempData["Error"] = "ERROR: This game doesn't exist or isn't for sale.";

                return RedirectToAction("Index");
            }

            BuyableDetailsVm buyableGame = new BuyableDetailsVm()
            {
                Game = _mapper.Map<GameListVm>(game),
                Categories = _mapper.Map<List<CategoryListVm>>(_categoryService.GetCategoriesFrom(gameId.Value)),
                PlayerTypes = _mapper.Map<List<PlayerTypeListVm>>(_typesService.GetPlayerTypesFrom(gameId.Value)),
                Filter = filter
            };

            return View(buyableGame);
        }
    }
}