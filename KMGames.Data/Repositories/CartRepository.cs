using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using KMGames.Entities.Entities;

namespace KMGames.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        //----------PROPERTIES----------//

        private readonly KmGamesDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public CartRepository(KmGamesDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        //----------METHODS----------//
        
        public void ClearCartFor(string userId)
        {
            var cart = GetCart(userId);

            cart.Games.Clear();

            _dbContext.Carts.Remove(cart);
        }


        public Cart CreateCart(string userId)
        {
            Cart cart = new Cart();
            
            cart.UserId = userId;

            _dbContext.Carts.Add(cart);

            return cart;
        }

        public Cart GetCart(string userId)
        {
            return _dbContext.Carts.Include(c => c.Games).FirstOrDefault(c => c.UserId == userId);
        }


        public CartListDto GetCartDto(string userId)
        {

            return _dbContext.Carts.Include(c => c.Games)
                                   .Where(c => c.UserId == userId)
                                   .Select(c => new CartListDto()
                                   {
                                       Items = c.Games.Select(g => g.Game).ToList()
                                   })
                                   .First();
        }

        public bool HasItems(string userId)
        {
            return _dbContext.Carts.Any(c => c.UserId == userId);
        }

        public void RemoveCartFrom(string userId)
        {
            Cart cart = GetCart(userId);

            _dbContext.Carts.Remove(cart);
        }

        public void RemoveGameFromCart(string userId, int gameId)
        {

            Cart cart = GetCart(userId);

            GameInCart gameInCart = _dbContext.GamesInCarts.FirstOrDefault(gic => gic.CartId == cart.CartId && gic.GameId == gameId);

            cart.Games.Remove(gameInCart);

            _dbContext.Entry(cart).State = EntityState.Modified;

        }
    }
}
