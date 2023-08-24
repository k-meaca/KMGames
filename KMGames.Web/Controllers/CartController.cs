using AutoMapper;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using KMGames.Web.App_Start;
using KMGames.Web.Models.Cart;
using KMGames.Web.ViewModel.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.Controllers
{
    public class CartController : Controller
    {
        //----------PROPERTIES----------//

        private Cart _cart;

        private IGameService _gameService;
        private ISaleService _saleService;
        private IUserService _userService;

        private IMapper _mapper;

        //----------CONSTRUCTOR----------/

        public CartController(IGameService gameService, ISaleService saleService, IUserService userService)
        {
            _gameService = gameService;
            _saleService = saleService;
            _userService = userService;

            _mapper = AutoMapperConfig.Mapper;
        }

        //----------METHODS----------//

        private Cart GetCart()
        {
            if(_cart != null)
            {
                return _cart;
            }

            _cart = (Cart)Session["cart"];

            if(_cart is null)
            {
                _cart = new Cart();
                Session["cart"] = _cart;
            }

            return _cart;
        }

        // GET: Index

        public ActionResult Index()
        {
            _cart = GetCart();

            if(_cart.Count() == 0)
            {
                return View("EmptyCart");
            }

            List<ItemCartVm> items = _mapper.Map<List<ItemCartVm>>(_cart.Items());

            CartVm cart = new CartVm()
            {
                Items = items,
                LastCategory = _cart.LastCategory
            };

            return View(cart);
        }

        // GET: Cart
        public ActionResult ShowCart()
        {
            _cart = GetCart();

            return PartialView("_ShoppingCart",_cart);
        }

        public ActionResult AddGame(int? gameId, string category)
        {
            if(gameId is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var game = _gameService.GetGame(gameId.Value);

            if(game is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }

            _cart = GetCart();

            ItemCart item = new ItemCart(game);

            _cart.AddGame(item);

            _cart.LastCategory = category;

            Session["cart"] = _cart; 

            return RedirectToAction("Index");
        }

        public ActionResult RemoveGame(int? gameId)
        {
            if(gameId is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            _cart = GetCart();

            _cart.RemoveGame(gameId.Value);

            Session["cart"] = _cart;

            return RedirectToAction("Index");
        }

        public ActionResult PayCart()
        {
            _cart = GetCart();

            var user = _userService.GetUserByEmail(User.Identity.Name);

            var games = _mapper.Map<List<Game>>(_cart.Items());

            try
            {
                _saleService.PayGames(user, games);

                _cart.Clear();

                Session["cart"] = _cart;

                TempData["Success"] = "SUCCESS: Payment was sucessful.";

                return View("EmptyCart");
            }
            catch (Exception)
            {
                TempData["Error"] = "ERROR: Something wrong happened.";

                return View("Index");
            }

        }
    }
}