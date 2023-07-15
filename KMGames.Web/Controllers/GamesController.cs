using AutoMapper;
using KMGames.Data.Migrations;
using KMGames.Entities.DTOs.PlayerType;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using KMGames.Web.App_Start;
using KMGames.Web.ViewModel.Categories;
using KMGames.Web.ViewModel.Games;
using KMGames.Web.ViewModel.PlayerType;
using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {

        //----------PROPERTIES----------//

        private readonly IGameService _gameService;
        private readonly IDeveloperService _developerService;
        private readonly ICategoryService _categoryService;
        private readonly IPLayerTypeService _playerTypeService;

        private readonly IMapper _mapper;

        //----------CONSTRUCTOR----------//

        public GamesController(IGameService gameService, IDeveloperService developer,
                ICategoryService categoryService, IPLayerTypeService pLayerTypeService)
        {
            _gameService = gameService;
            _developerService = developer;
            _categoryService = categoryService;
            _playerTypeService = pLayerTypeService;

            _mapper = AutoMapperConfig.Mapper;
        }

        //----------METHODS----------//

        private void LoadSelected(List<CategoryCheckVm> categories, ICollection<GameCategory> selected)
        {
            foreach(var item in selected)
            {
                categories.FirstOrDefault(c => c.CategoryId == item.CategoryId).Selected = true;
            }
        }

        private void LoadSelected(List<PlayerTypeCheckVm> types, ICollection<PlayerGame> selected)
        {
            foreach (var item in selected)
            {
                types.FirstOrDefault(c => c.PlayerTypeId == item.PlayerTypeId).Selected = true;
            }
        }

        //----------ACTIONS----------//

        // GET: Games
        public ActionResult Index(int? page, int? pageSize)
        {
            var gamesList = _gameService.GetGames();

            var gameListVm = _mapper.Map<List<GameListVm>>(gamesList);

            page = page ?? 1;
            pageSize = pageSize ?? 10;

            ViewBag.PageSize = pageSize;

            return View(gameListVm.ToPagedList(page.Value,pageSize.Value));
        }

        public ActionResult Create()
        {
            var categories = _categoryService.GetCheckBoxList();

            var categoriesCheckVm = _mapper.Map<List<CategoryCheckVm>>(categories);

            var types = _playerTypeService.GetCheckBoxList();

            var typesCheckVm = _mapper.Map<List<PlayerTypeCheckVm>>(types);

            var gameEditVm = new GameEditVm()
            {
                Developers = _developerService.GetDropDownList(),
                Categories = categoriesCheckVm,
                PlayerTypes = typesCheckVm,
                Release = DateTime.Now.Date
            };

            return View(gameEditVm);
        }

        [HttpPost]
        public ActionResult Create(GameEditVm gameEditVm)
        {
            if (!ModelState.IsValid)
            {
                gameEditVm.Developers = _developerService.GetDropDownList();

                return View(gameEditVm);
            }

            var game = _mapper.Map<Game>(gameEditVm);

            if (_gameService.Exist(game))
            {
                gameEditVm.Developers = _developerService.GetDropDownList();

                return View(gameEditVm);
            }

            List<GameCategory> gameCategories = new List<GameCategory>();
            List<PlayerGame> playersGames = new List<PlayerGame>();

            gameEditVm.Categories.ForEach(c =>
            {
                if (c.Selected) gameCategories.Add(new GameCategory() { CategoryId = c.CategoryId });
            });

            gameEditVm.PlayerTypes.ForEach(pg =>
            {
                if (pg.Selected) playersGames.Add(new PlayerGame() { PlayerTypeId = pg.PlayerTypeId });
            });

            game.GameCategories = gameCategories;
            game.PlayersGame = playersGames; 
            
            try 
            {
                _gameService.Add(game);
                TempData["Success"] = "SUCCESS: Game added successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"ERROR: An error ocurred while trying to save data.\n{ex.Message}";

                gameEditVm.Developers = _developerService.GetDropDownList();

                return View(gameEditVm);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if(id is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var game = _gameService.GetGame(id.Value);

            if (game == null)
            {
                TempData["Error"] = "ERROR: This game was deleted by other user. It can't be deleted.";

                return RedirectToAction("Index");
            }

            var categories = _categoryService.GetCheckBoxList();
            var categoriesVm = _mapper.Map<List<CategoryCheckVm>>(categories);

            var types = _playerTypeService.GetCheckBoxList();
            var typesVm = _mapper.Map<List<PlayerTypeCheckVm>>(types);

            var developers = _developerService.GetDropDownList();
            var developersVm = _mapper.Map<List<SelectListItem>>(developers);

            LoadSelected(categoriesVm, game.GameCategories);
            LoadSelected(typesVm, game.PlayersGame);

            var gameEditVm = _mapper.Map<GameEditVm>(game);

            gameEditVm.Developers = developersVm;
            gameEditVm.Categories = categoriesVm;
            gameEditVm.PlayerTypes = typesVm;

            return View(gameEditVm);
        }

        [HttpPost]
        public ActionResult Edit(GameEditVm gameEditVm)
        {
            if (!ModelState.IsValid)
            {
                gameEditVm.Developers = _developerService.GetDropDownList();

                return View(gameEditVm);
            }

            gameEditVm.GameCategories = new List<GameCategory>();
            gameEditVm.PlayersGame = new List<PlayerGame>();

            gameEditVm.Categories.ForEach(c =>
            {
                if (c.Selected) gameEditVm.GameCategories.Add(new GameCategory() { CategoryId = c.CategoryId });
            });

            gameEditVm.PlayerTypes.ForEach(pg =>
            {
                if (pg.Selected) gameEditVm.PlayersGame.Add(new PlayerGame() { PlayerTypeId = pg.PlayerTypeId });
            });

            var game = _mapper.Map<Game>(gameEditVm);

            if (_gameService.Exist(game))
            {
                gameEditVm.Developers = _developerService.GetDropDownList();

                return View(gameEditVm);
            }

            try
            {
                _gameService.Edit(game);

                TempData["Success"] = "SUCCESS: Game edited successfully";

            }
            catch (Exception ex)
            {
                TempData["Error"] = $"ERROR: An error ocurred while trying to edit game.\n{ex.Message}";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if(id is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var game = _gameService.GetGame(id.Value);

            if(game is null)
            {
                TempData["Error"] = "ERROR: This game was deleted by other user.";

                return RedirectToAction("Index");
            }
            else if (_gameService.ItsRelated(id.Value))
            {
                TempData["Error"] = "ERROR: This game is related to some sales.";

                return RedirectToAction("Index");
            }

            var gameListVm = _mapper.Map<GameListVm>(game);

            return View(gameListVm);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var game = _gameService.GetGame(id);

            try
            {
                _gameService.Delete(game);

                TempData["Warning"] = $"WARNING: {game.Title} deleted successfully";
            }
            catch(Exception ex)
            {
                TempData["Error"] = $"ERROR: An error ocurred while trying to delete the game.\n{ex.Message}";
            }

            return RedirectToAction("Index");
        }

    }
}