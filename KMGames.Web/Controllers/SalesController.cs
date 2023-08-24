using AutoMapper;
using KMGames.Services.Interfaces;
using KMGames.Web.App_Start;
using KMGames.Web.ViewModel.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.Controllers
{
    public class SalesController : Controller
    {

        private readonly ISaleService _saleService;
        private readonly IUserService _userService;
        private readonly IGameService _gameService;
        private readonly ICategoryService _categoryService;

        private readonly IMapper _mapper;

        public SalesController(ISaleService saleService, IUserService userService,
            IGameService gameService, ICategoryService categoryService)
        {
            _saleService = saleService;
            _userService = userService;
            _gameService = gameService;
            _categoryService = categoryService;
            
            _mapper = AutoMapperConfig.Mapper;
        }

        // GET: Sales
        public ActionResult Index()
        {
            var sales = _saleService.GetSales();

            var salesListVm = _mapper.Map<List<SaleListVm>>(sales);

            return View(salesListVm);
        }

        public ActionResult SalesCustomer()
        {

            var customerSales = _saleService.GetCustomerSales();

            var customerSalesVm = _mapper.Map<List<CustomerSalesListVm>>(customerSales);

            return View(customerSalesVm);
        }
    }
}