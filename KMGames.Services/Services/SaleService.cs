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
    }
}
