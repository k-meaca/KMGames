using KMGames.Data;
using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.Sale;
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
    public class SaleService : ISaleService
    {
        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;

        private readonly ISaleRepository _saleRepository;

        //----------CONSTRUCTOR----------//

        public SaleService(IUnitOfWork unitOfWork, ISaleRepository saleRepository)
        {
            _unitOfWork = unitOfWork;
            _saleRepository = saleRepository;
        }

        //----------METHODS----------//
        public Sale GetSale(int id)
        {
            return _saleRepository.GetSale(id);
        }

        public ICollection<SaleListDto> GetSales()
        {
            return _saleRepository.GetSales();
        }

        public ICollection<CustomerSalesListDto> GetCustomerSales()
        {
            return _saleRepository.GetCustomerSales();
        }

        public void PayGames(User user, List<Game> games)
        {
            try
            {
                using(var transaction = new TransactionScope())
                {
                    var sale = _saleRepository.MakeSale(user);

                    _saleRepository.PayGames(sale, games);

                    _unitOfWork.SaveChanges();

                    transaction.Complete();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
