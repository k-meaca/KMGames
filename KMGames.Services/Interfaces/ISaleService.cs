using KMGames.Entities.DTOs.Sale;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Services.Interfaces
{
    public interface ISaleService
    {
        ICollection<SaleListDto> GetSales();

        Sale GetSale(int it);

        void PayGames(User user, List<Game> games);

    }
}
