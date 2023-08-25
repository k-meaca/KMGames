using KMGames.Data;
using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.Cart;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace KMGames.Services.Services
{
    public class CartService : ICartService
    {
        //----------PROPERTIES----------//

        private readonly ICartRepository _cartRepository;

        private readonly IUnitOfWork _unitOfWork;

        //----------CONSTRUCTOR----------//

        public CartService(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }

        //----------METHODS----------//

        public void AddGameToCart(string userId, Game game)
        {
            try
            {
                Cart cart = _cartRepository.GetCart(userId);

                GameInCart gameInCart = new GameInCart()
                {
                    CartId = cart.CartId,
                    GameId = game.GameId
                };

                cart.Games.Add(gameInCart);

                _unitOfWork.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ClearCartFor(string userId)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    _cartRepository.ClearCartFor(userId);

                    _unitOfWork.SaveChanges();

                    transaction.Complete();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void CreateCart(string userId, Game game)
        {
            try
            {
                using(var transaction = new TransactionScope())
                {
                    Cart cart = _cartRepository.CreateCart(userId);

                    GameInCart gameInCart = new GameInCart()
                    {
                        CartId = cart.CartId,
                        GameId = game.GameId
                    };

                    cart.Games.Add(gameInCart);

                    _unitOfWork.SaveChanges();

                    transaction.Complete();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }


        public CartListDto GetCart(string userId)
        {
            return _cartRepository.GetCartDto(userId);
        }

        public bool HasItems(string userId)
        {
            return _cartRepository.HasItems(userId);
        }

        public void RemoveCartFrom(string userId)
        {
            try
            {
                _cartRepository.RemoveCartFrom(userId);

                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveGameFromCart(string userId, int gameId)
        {
            try
            {
                _cartRepository.RemoveGameFromCart(userId, gameId);

                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
