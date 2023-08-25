using KMGames.Entities.DTOs.Cart;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Data.Interfaces
{
    public interface ICartRepository
    {
        void ClearCartFor(string userId);
        Cart CreateCart(string userId);
        Cart GetCart(string userId);
        CartListDto GetCartDto(string userId);
        bool HasItems(string userId);
        void RemoveCartFrom(string userId);
        void RemoveGameFromCart(string userId, int gameId);
    }
}
