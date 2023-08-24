using AutoMapper;
using KMGames.Services.Interfaces;
using KMGames.Web.App_Start;
using KMGames.Web.ViewModel.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.Controllers
{
    public class HomeController : Controller
    {
        //----------PROPERTIES----------//

        private readonly IGameService _gameService;

        private readonly IMapper _mapper;

        //----------CONSTRUCTOR---------//

        public HomeController(IGameService gameService)
        {
            _gameService = gameService;

            _mapper = AutoMapperConfig.Mapper;
        }

        public ActionResult Index()
        {
            int numberOfGames = 3;

            var bestSeller = _gameService.GetBestSeller(numberOfGames);

            var bestSellerVm = _mapper.Map<List<GameListVm>>(bestSeller);

            return View(bestSellerVm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}