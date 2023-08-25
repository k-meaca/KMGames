using KMGames.Entities.DTOs.Cart;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Services.Interfaces
{
    public interface ICartService
    {
        void AddGameToCart(string userId, Game game);
        
        void ClearCartFor(string userId);

        void CreateCart(string userId, Game game);
        
        CartListDto GetCart(string userId);

        bool HasItems(string userId);

        void RemoveCartFrom(string userId);
        
        void RemoveGameFromCart(string userId, int gameId);
    }
}
