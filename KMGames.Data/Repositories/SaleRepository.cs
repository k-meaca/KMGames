using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.Sale;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGames.Data.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        //----------PROPERTIES----------//

        private readonly KmGamesDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public SaleRepository(KmGamesDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        //----------METHODS----------//
        public Sale GetSale(int id)
        {
            return _dbContext.Sales.FirstOrDefault(s => s.SaleId == id);
        }

        public ICollection<SaleListDto> GetSales()
        {
            return _dbContext.Sales.Select(s =>
                                    new SaleListDto()
                                    {
                                        SaleId = s.SaleId,
                                        User = s.User.LastName + ", " + s.User.FirstName,
                                        Date = s.Date,
                                        Total = s.Total
                                    }
                                )
                                .ToList();
        }

        public Sale MakeSale(User user)
        {
            Sale sale = new Sale()
            {
                UserId = user.UserId,
                Date = DateTime.Now,
            };

            _dbContext.Sales.Add(sale);

            return sale;
        }

        public void PayGames(Sale sale, List<Game> games)
        {

            foreach(var game in games)
            {
                var saleDetails = new SaleDetail()
                {
                    SaleId = sale.SaleId,
                    GameId = game.GameId,
                    SalePrice = game.ActualPrice
                };

                _dbContext.SalesDetais.Add(saleDetails);
            }

            sale.Total = games.Sum(g => g.ActualPrice);
        }
    }
}
