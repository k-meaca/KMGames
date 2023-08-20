using KMGames.Entities.DTOs.Sale;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Data.Interfaces
{
    public interface ISaleRepository
    {
        ICollection<SaleListDto> GetSales();

        Sale GetSale(int id);

        Sale MakeSale(User user);

        void PayGames(Sale sale, List<Game> games);
    }
}
