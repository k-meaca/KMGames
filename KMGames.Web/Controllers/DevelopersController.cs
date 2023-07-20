using AutoMapper;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using KMGames.Utilities;
using KMGames.Web.App_Start;
using KMGames.Web.ViewModel.Categories;
using KMGames.Web.ViewModel.Cities;
using KMGames.Web.ViewModel.Developers;
using KMGames.Web.ViewModel.Games;
using KMGames.Web.ViewModel.PlayerType;
using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.Controllers
{
    [Authorize]
    public class DevelopersController : Controller
    {

        //----------PROPERTIES----------//

        private readonly IDeveloperService _developerService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;
        private readonly IPLayerTypeService _typeService;

        private readonly IMapper _mapper;

        //----------CONSTRUCTOR----------//

        public DevelopersController(IDeveloperService developerService, ICountryService countryService,
                            ICityService cityService, IGameService gameService, ICategoryService categoryService,
                            IPLayerTypeService playerTypeService)
        {
            _developerService = developerService;
            _countryService = countryService;
            _cityService = cityService;
            _gameService = gameService;
            _categoryService = categoryService;
            _typeService = playerTypeService;

            _mapper = AutoMapperConfig.Mapper;
        }

        //----------METHODS----------//

        public JsonResult GetCities(int countryId)
        {
            var cities = _cityService.GetCities(countryId);

            var citiesListVm = _mapper.Map<List<CityListVm>>(cities);

            return Json(citiesListVm);
        }

        public ActionResult SetListOnDeveloperEditVm(DeveloperEditVm developerEditVm)
        {
            developerEditVm.Countries = _countryService.GetDropDownList();
            developerEditVm.Cities = _cityService.GetDropDownListByCountry(developerEditVm.CountryId);

            return View(developerEditVm);
        }


        private void LoadSelected(List<CategoryCheckVm> categories, ICollection<GameCategory> selected)
        {
            foreach (var item in selected)
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

        // GET: Developers
        public ActionResult Index(int? page, int? pageSize, string SortBy="Developer")
        {
            page = page ?? 1;
            pageSize = pageSize ?? 10;

            ViewBag.PageSize = pageSize;

            var developersList = _developerService.GetDevelopers();

            var developersListVm = _mapper.Map<List<DeveloperListVm>>(developersList);

            if(SortBy == "Developer")
            {
                developersListVm = developersListVm.OrderBy(d => d.Name).ToList();
            }
            else if(SortBy=="Country")
            {
                developersListVm = developersListVm.OrderBy(d => d.Country).ThenBy(d => d.City).ToList();
            }

            var developerSortVm = new DeveloperSortVm()
            {
                Developers = developersListVm.ToPagedList(page.Value,pageSize.Value),

                Sorts = new Dictionary<string, string>()
                {
                    { "By developer", "Developer" },
                    { "By country", "Country" }
                },

                SortBy = SortBy
            };

            return View(developerSortVm);
        }

        public ActionResult Create()
        {
            var developerEditVm = new DeveloperEditVm()
            {
                Countries = _countryService.GetDropDownList(),
                Cities = new List<SelectListItem>()
            };

            return View(developerEditVm);
        }

        [HttpPost]
        public ActionResult Create(DeveloperEditVm developerEditVm)
        {
            if (!ModelState.IsValid)
            {
                return SetListOnDeveloperEditVm(developerEditVm);
            }

            var developer = _mapper.Map<Developer>(developerEditVm);

            if (_developerService.Exist(developer))
            {
                ModelState.AddModelError(string.Empty, $"{developerEditVm.Name} already exist");

                return SetListOnDeveloperEditVm(developerEditVm);
            }

            _developerService.Add(developer);

            TempData["Success"] = "SUCCESS: Developer added successfully";

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var developer = _developerService.GetDeveloper(id.Value);

            if (developer is null)
            {
                TempData["Error"] = "ERROR: This developer has been deleted by another user.";

                return RedirectToAction("index");
            }
            else if (_developerService.ItsRelated(id.Value))
            {
                TempData["Error"] = "ERROR: This developer is related to some games. It can't be deleted.";

                return RedirectToAction("index");
            }

            var developerListVm = _mapper.Map<DeveloperListVm>(developer);

            return View(developerListVm);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var developer = _developerService.GetDeveloper(id);

            if(developer is null)
            {
                TempData["Error"] = "ERROR: This developer was deleted by other user.";
            }
            else
            {
                _developerService.Delete(developer);

                TempData["Warning"] = $"WARNING: {developer.Name} deleted successfully";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var developer = _developerService.GetDeveloper(id.Value);

            if (developer is null)
            {
                TempData["Error"] = "ERROR: This developer has been deleted by another user.";

                return RedirectToAction("index");
            }

            var developerEditVm = _mapper.Map<DeveloperEditVm>(developer);

            return SetListOnDeveloperEditVm(developerEditVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DeveloperEditVm developerEditVm)
        {
            if (!ModelState.IsValid)
            {
                return SetListOnDeveloperEditVm(developerEditVm);
            }

            var developer = _mapper.Map<Developer>(developerEditVm);

            if (_developerService.Exist(developer))
            {
                ModelState.AddModelError(string.Empty, "This developer already exist.");

                return SetListOnDeveloperEditVm(developerEditVm);
            }

            _developerService.Edit(developer);

            TempData["Success"] = "SUCCESS: Developer edited successfully";

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if(id is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var developer = _developerService.GetDeveloper(id.Value);

            if(developer is null)
            {
                TempData["Error"] = "This developer was deleted by other user.";

                return RedirectToAction("Index");
            }

            var developerListVm = _mapper.Map<DeveloperListVm>(developer);

            var gamesListVm = _gameService.GetGames(developer.DeveloperId);

            var developerDetailVm = new DeveloperDetailVm()
            {
                Developer = developerListVm,

                Games = _mapper.Map<List<GameListVm>>(gamesListVm)
            };

            return View(developerDetailVm);
        }

        public ActionResult AddGame(int? developerId)
        {
            if(developerId is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var developer = _developerService.GetDeveloper(developerId.Value);

            if(developer is null)
            {
                TempData["Error"] = "ERROR: This developer was deleted by other user.";

                return RedirectToAction($"/Details/{developerId.Value}");
            }

            var gameEditVm = new GameEditVm()
            {
                DeveloperId = developer.DeveloperId,
                Developers = _developerService.GetDropDownList(),
                PlayerTypes = _mapper.Map<List<PlayerTypeCheckVm>>(_typeService.GetCheckBoxList()),
                Categories = _mapper.Map<List<CategoryCheckVm>>(_categoryService.GetCheckBoxList()),
                Release = DateTime.Today.Date
            };

            return View(gameEditVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGame(GameEditVm gameEditVm)
        {
            if (!ModelState.IsValid)
            {
                gameEditVm.Developers = _developerService.GetDropDownList();
                gameEditVm.PlayerTypes = _mapper.Map<List<PlayerTypeCheckVm>>(_typeService.GetCheckBoxList());
                gameEditVm.Categories = _mapper.Map<List<CategoryCheckVm>>(_categoryService.GetCheckBoxList());

                return View(gameEditVm);
            }

            var game = _mapper.Map<Game>(gameEditVm);

            if (_gameService.Exist(game))
            {
                ModelState.AddModelError(string.Empty, "This game already exist in database.");

                gameEditVm.Developers = _developerService.GetDropDownList();
                gameEditVm.PlayerTypes = _mapper.Map<List<PlayerTypeCheckVm>>(_typeService.GetCheckBoxList());
                gameEditVm.Categories = _mapper.Map<List<CategoryCheckVm>>(_categoryService.GetCheckBoxList());

                return View(gameEditVm);
            }

            try
            {
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

                if (gameEditVm.imageFile != null)
                {

                    var extension = Path.GetExtension(gameEditVm.imageFile.FileName);
                    var fileName = Guid.NewGuid().ToString();
                    var file = $"{fileName}{extension}";
                    var response = FileHelper.UploadPhoto(gameEditVm.imageFile, WC.GameImagesFolder, file);
                    game.Image = file;
                }

                _gameService.Add(game);

                TempData["Success"] = "SUCCESS: Game added successfully into selected developer.";
            }
            catch(Exception ex)
            {
                TempData["Error"] = $"ERROR: An error ocurred while trying to add a new game.\n{ex.Message}";
            }

            return RedirectToAction($"/Details/{game.DeveloperId}");
        }


        public ActionResult DeleteGame(int? gameId, int? developerId)
        {
            if(gameId is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            
            if (developerId is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var game = _gameService.GetGame(gameId.Value);

            if(game is null)
            {
                TempData["Error"] = "ERROR: This game was deleted by other user";

                return RedirectToAction($"/Detail/{developerId.Value}");
            }
            else if (_gameService.ItsRelated(gameId.Value))
            {
                TempData["Error"] = "ERROR: This game is related to some sales. It can't be deleted.";

                return RedirectToAction($"/Details/{developerId.Value}");
            }

            var gameEditVm = _mapper.Map<GameEditVm>(game);

            gameEditVm.Developers = _developerService.GetDropDownList();
            gameEditVm.PlayerTypes = _mapper.Map<List<PlayerTypeCheckVm>>(_typeService.GetCheckBoxList());
            gameEditVm.Categories = _mapper.Map<List<CategoryCheckVm>>(_categoryService.GetCheckBoxList());

            return View(gameEditVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGame(int gameId, int developerId)
        {
            var game = _gameService.GetGame(gameId);
            try
            {
                if (game.Image != null)
                {
                    FileHelper.DeletePhoto(WC.GameImagesFolder + game.Image);
                }

                _gameService.Delete(game);

                TempData["Warning"] = "WARNING: Game deleted successfully from this developer.";
            }
            catch(Exception ex)
            {
                TempData["ERROR"] = $"ERROR: An error ocurred while trying to delete this game.\n{ex.Message}";
            }

            return RedirectToAction($"/Details/{developerId}");
        }

        public ActionResult EditGame(int? gameId, int? developerId)
        {
            if (gameId is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (developerId is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var game = _gameService.GetGame(gameId.Value);

            if (game is null)
            {
                TempData["Error"] = "ERROR: This game was deleted by other user";

                return RedirectToAction($"/Details/{developerId.Value}");
            }

            var categories = _categoryService.GetCheckBoxList();
            var categoriesVm = _mapper.Map<List<CategoryCheckVm>>(categories);

            var types = _typeService.GetCheckBoxList();
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
        [ValidateAntiForgeryToken]
        public ActionResult EditGame(GameEditVm gameEditVm)
        {
            if (!ModelState.IsValid)
            {
                gameEditVm.Developers = _developerService.GetDropDownList();
                gameEditVm.PlayerTypes = _mapper.Map<List<PlayerTypeCheckVm>>(_typeService.GetCheckBoxList());
                gameEditVm.Categories = _mapper.Map<List<CategoryCheckVm>>(_categoryService.GetCheckBoxList());

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
                ModelState.AddModelError(string.Empty, "This game already exist in database.");

                gameEditVm.Developers = _developerService.GetDropDownList();
                gameEditVm.PlayerTypes = _mapper.Map<List<PlayerTypeCheckVm>>(_typeService.GetCheckBoxList());
                gameEditVm.Categories = _mapper.Map<List<CategoryCheckVm>>(_categoryService.GetCheckBoxList());

                return View(gameEditVm);
            }

            try
            {
                if (gameEditVm.imageFile != null)
                {
                    if (gameEditVm.Image != null)
                    {
                        FileHelper.DeletePhoto(WC.GameImagesFolder + gameEditVm.Image);
                    }

                    var extension = Path.GetExtension(gameEditVm.imageFile.FileName);
                    var fileName = Guid.NewGuid().ToString();
                    var file = $"{fileName}{extension}";
                    var response = FileHelper.UploadPhoto(gameEditVm.imageFile, WC.GameImagesFolder, file);
                    game.Image = file;
                }

                _gameService.Edit(game);

                TempData["Success"] = "SUCCESS: Game added successfully into selected developer.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"ERROR: An error ocurred while trying to add a new game.\n{ex.Message}";
            }

            return RedirectToAction($"/Details/{game.DeveloperId}");
        }
    }
}