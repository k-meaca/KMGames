using AutoMapper;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using KMGames.Web.App_Start;
using KMGames.Web.Models.SessionCart;
using KMGames.Web.ViewModel.SessionCart;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.Controllers
{
    public class SessionCartController : Controller
    {
        //----------PROPERTIES----------//

        private SessionCart _cart;

        private IGameService _gameService;
        private ISaleService _saleService;
        private IUserService _userService;
        private ICartService _cartService;

        private IMapper _mapper;

        //----------CONSTRUCTOR----------/

        public SessionCartController(IGameService gameService, ISaleService saleService, IUserService userService, ICartService cartService)
        {
            _gameService = gameService;
            _saleService = saleService;
            _userService = userService;
            _cartService = cartService;

            _mapper = AutoMapperConfig.Mapper;
        }

        //----------METHODS----------//

        private SessionCart GetCart()
        {
            _cart = (SessionCart)Session["cart"];

            if(_cart is null)
            {
                _cart = new SessionCart();

                if (_cartService.HasItems(User.Identity.GetUserId()))
                {
                    var cart = _cartService.GetCart(User.Identity.GetUserId());

                    _cart.Items = _mapper.Map<List<ItemCart>>(cart.Items);
                }

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

            List<ItemCartVm> items = _mapper.Map<List<ItemCartVm>>(_cart.Items);

            CartVm cart = new CartVm()
            {
                Items = items,
                LastCategory = _cart.LastCategory
            };

            return View(cart);
        }

        // GET: SessionCart
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

            if (_cart.Count() == 1)
            {
                _cartService.CreateCart(User.Identity.GetUserId(), game);
            }
            else
            {
                _cartService.AddGameToCart(User.Identity.GetUserId(), game);
            }

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

            if(_cart.Count() == 0)
            {
                _cartService.RemoveCartFrom(User.Identity.GetUserId());
            }
            else
            {
                _cartService.RemoveGameFromCart(User.Identity.GetUserId(), gameId.Value);
            }

            return RedirectToAction("Index");
        }

        public ActionResult PayCart()
        {
            _cart = GetCart();

            var user = _userService.GetUserByEmail(User.Identity.Name);

            var games = _mapper.Map<List<Game>>(_cart.Items);

            try
            {
                _saleService.PayGames(user, games);

                _cart.Clear();

                _cartService.ClearCartFor(User.Identity.GetUserId());

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