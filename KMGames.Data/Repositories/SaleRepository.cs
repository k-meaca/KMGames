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
                                        User = s.User.NickName,
                                        Date = s.Date,
                                        Total = s.Total
                                    }
                                )
                                .ToList();
        }
    }
}
