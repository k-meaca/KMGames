using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.Sale;
using KMGames.Entities.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

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
            return _dbContext.Sales.Include(s => s.User).
                                    Select(s =>
                                        new SaleListDto()
                                        {
                                            SaleId = s.SaleId,
                                            UserId = s.User.UserId,
                                            UserEmail = s.User.Email,
                                            UserName = s.User.FirstName + " " + s.User.LastName,
                                            Date = s.Date,
                                            Total = s.Total
                                        }
                                    )
                                    .ToList();
        }

        public ICollection<CustomerSalesListDto> GetCustomerSales()
        {
            var customerSales = _dbContext.Sales.Include(s => s.User)
                                   .GroupBy(s => s.User, s=> s.UserId)
                                   .Select(s => new CustomerSalesListDto()
                                   {
                                       UserId = s.Key.UserId,
                                       UserEmail = s.Key.Email,
                                       UserName = s.Key.FirstName + " " + s.Key.LastName,
                                       TotalSpent = s.Key.Sales.Sum(sale => sale.Total),
                                   })
                                   .ToList();

            customerSales.ForEach(c =>
            {
                c.PurchasedGames = _dbContext.SalesDetais.Include(sd => sd.Sale).Count(sd => sd.Sale.UserId == c.UserId);
            });

            return customerSales;
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
