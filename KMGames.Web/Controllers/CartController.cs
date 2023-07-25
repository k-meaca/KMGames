using KMGames.Web.Models.Cart;
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

            return View(_cart);
        }

        // GET: Cart
        public ActionResult ShowCart()
        {
            _cart = GetCart();

            return PartialView("_ShoppingCart",_cart);
        }
    }
}