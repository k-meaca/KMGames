using AutoMapper;
using KMGames.Entities.DTOs.City;
using KMGames.Entities.DTOs.Developer;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using KMGames.Web.App_Start;
using KMGames.Web.ViewModel.Cities;
using KMGames.Web.ViewModel.Developers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.Controllers
{
    [Authorize]
    public class CitiesController : Controller
    {
        //----------PROPERTIES----------//

        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IDeveloperService _developerService;

        private readonly IMapper _mapper;

        //---------CONSTRUCTOR----------//

        public CitiesController(ICountryService countryService, ICityService cityService,
                        IDeveloperService developerService)
        {
            _countryService = countryService;
            _cityService = cityService;
            _developerService = developerService;

            _mapper = AutoMapperConfig.Mapper;
        }

        //----------ACTIONS----------//

        // GET: Cities
        public ActionResult Index(int? page, int? pageSize, int? filterBy)
        {
            List<CityListVm> citiesListVm;
            List<CityListDto> citiesList;

            page = page ?? 1;
            pageSize = pageSize ?? 10;

            ViewBag.Pagesize = pageSize;
            
            if(filterBy is null)
            {
                citiesList = _cityService.GetCities().ToList();
            }
            else
            {
                citiesList = _cityService.GetCities().Where(c => c.CountryId == filterBy.Value).ToList();
            }

            citiesListVm = _mapper.Map<List<CityListVm>>(citiesList);

            var cityFilterVm = new CityFilterVm
            {
                Countries = _countryService.GetDropDownList(),

                Cities = citiesListVm.ToPagedList(page.Value, pageSize.Value),

                FilterBy = filterBy
            };

            return View(cityFilterVm);
        }

        public ActionResult Create()
        {
            var countriesList = _countryService.GetCountries();

            var dropDownCountries = countriesList.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.CountryId.ToString()
            }).ToList(); ;

            var cityEditVm = new CityEditVm()
            {
                Countries = dropDownCountries
            };

            return View(cityEditVm);
        }

        [HttpPost]
        public ActionResult Create(CityEditVm cityEditVm)
        {
            if (!ModelState.IsValid)
            {
                cityEditVm.Countries = _countryService.GetDropDownList();

                return View(cityEditVm);
            }

            var city = _mapper.Map<City>(cityEditVm);

            if (_cityService.Exist(city))
            {
                ModelState.AddModelError(string.Empty, $"{cityEditVm.Name} already exist in the selected country.");

                cityEditVm.Countries = _countryService.GetDropDownList();

                return View(cityEditVm);
            }

            _cityService.Add(city);

            TempData["Success"] = "SUCCESS: City added successfully";

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var city = _cityService.GetCity(id.Value);

            if (city is null)
            {
                TempData["Error"] = "ERROR: The city was deleted by other user.";

                return RedirectToAction("Index");
            }
            else if (_cityService.ItsRelated(id.Value))
            {
                TempData["Error"] = "ERROR: The city is related to some developers. It can't be deleted.";

                return RedirectToAction("Index");
            }

            var cityListVm = _mapper.Map<CityListVm>(city);

            return View(cityListVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var city = _cityService.GetCity(id);

            _cityService.Delete(city);

            TempData["Warning"] = "WARNING: City deleted successfully from database.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var city = _cityService.GetCity(id.Value);

            if (city is null)
            {
                TempData["Error"] = "ERROR: The city was deleted by other user.";

                return RedirectToAction("Index");
            }

            var cityEditVm = _mapper.Map<CityEditVm>(city);

            cityEditVm.Countries = _countryService.GetDropDownList();

            return View(cityEditVm);
        }

        [HttpPost]
        public ActionResult Edit(CityEditVm cityEditVm)
        {
            if (!ModelState.IsValid)
            {
                cityEditVm.Countries = _countryService.GetDropDownList();

                return View(cityEditVm);
            }

            var city = _mapper.Map<City>(cityEditVm);

            if (_cityService.Exist(city))
            {
                ModelState.AddModelError(string.Empty, $"{city.Name} already exist.");

                cityEditVm.Countries = _countryService.GetDropDownList();

                return View(cityEditVm);
            }

            _cityService.Edit(city);

            TempData["Success"] = "SUCCESS: City edited successfully.";

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var city = _cityService.GetCity(id.Value);

            if (city is null)
            {
                TempData["Error"] = $"{city.Name} was deleted by other user.";

                return RedirectToAction("Index");
            }

            var cityListVm = _mapper.Map<CityListVm>(city);

            var developersListVm = _mapper.Map<List<DeveloperListVm>>(_developerService.GetDevelopers(id.Value));

            var cityDetailsVm = new CityDetailsVm()
            {
                City = cityListVm,
                Developers = developersListVm
            };

            return View(cityDetailsVm);
        }

        public ActionResult AddDeveloper(int? cityId)
        {
            if (cityId is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var city = _cityService.GetCity(cityId.Value);

            if (city is null)
            {
                TempData["Error"] = "ERROR: This city was deleted by other user. It's not posible to add any developer to it.";

                return RedirectToAction($"/Details/{cityId.Value}");
            }

            var developerEditVm = new DeveloperEditVm()
            {
                CityId = cityId.Value,
                Cities = _cityService.GetDropDownListByCountry(city.CountryId),
                CountryId = city.CountryId,
                Countries = _countryService.GetDropDownList()
            };

            return View(developerEditVm);
        }

        [HttpPost]
        public ActionResult AddDeveloper(DeveloperEditVm developerEditVm)
        {
            if (!ModelState.IsValid)
            {
                developerEditVm.Cities = _cityService.GetDropDownListByCountry(developerEditVm.CountryId);
                developerEditVm.Countries = _countryService.GetDropDownList();

                return View(developerEditVm);
            }

            var developer = _mapper.Map<Developer>(developerEditVm);

            if (_developerService.Exist(developer))
            {
                ModelState.AddModelError(string.Empty, "This developer already exist in database.");

                developerEditVm.Cities = _cityService.GetDropDownListByCountry(developerEditVm.CountryId);
                developerEditVm.Countries = _countryService.GetDropDownList();

                return View(developerEditVm);
            }

            try
            {
                _developerService.Add(developer);

                TempData["Success"] = "SUCCESS: Developer added successfully in selected country.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"ERROR: An error ocurred while trying to add the developer.\n{ex.Message}";
            }

            return RedirectToAction($"/Details/{developer.CityId}");

        }

        public ActionResult DeleteDeveloper(int? developerId, int? cityId)
        {
            if (developerId is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (cityId is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var developer = _developerService.GetDeveloper(developerId.Value);

            if (developer is null)
            {
                TempData["ERROR"] = "ERROR: This developer was deleted by other user.";

                return RedirectToAction($"/Details/{cityId.Value}");
            }
            else if (_developerService.ItsRelated(developerId.Value))
            {
                TempData["ERROR"] = "ERROR: This developer is related to some games. It can't be deleted.";

                return RedirectToAction($"/Details/{cityId.Value}");
            }

            var developerEditVm = _mapper.Map<DeveloperEditVm>(developer);

            developerEditVm.Cities = _cityService.GetDropDownListByCountry(developer.CountryId);
            developerEditVm.Countries = _countryService.GetDropDownList();

            return View(developerEditVm);
        }

        [HttpPost, ActionName("DeleteDeveloper")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDeveloper(int developerId)
        {
            var developer = _developerService.GetDeveloper(developerId);

            _developerService.Delete(developer);

            TempData["Warning"] = $"WARNING: {developer.Name} was deleted from database.";

            return RedirectToAction($"/Details/{developer.CityId}");
        }

        public ActionResult EditDeveloper(int? developerId, int? cityId)
        {
            if (developerId is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (cityId is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var developer = _developerService.GetDeveloper(developerId.Value);

            if (developer is null)
            {
                TempData["ERROR"] = "ERROR: This developer was deleted by other user.";

                return RedirectToAction($"/Details/{cityId.Value}");
            }

            var developerEditVm = _mapper.Map<DeveloperEditVm>(developer);

            developerEditVm.Cities = _cityService.GetDropDownListByCountry(developer.CountryId);
            developerEditVm.Countries = _countryService.GetDropDownList();

            return View(developerEditVm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDeveloper(DeveloperEditVm developerEditVm)
        {
            if (!ModelState.IsValid)
            {
                developerEditVm.Cities = _cityService.GetDropDownListByCountry(developerEditVm.CountryId);
                developerEditVm.Countries = _countryService.GetDropDownList();

                return View(developerEditVm);
            }

            var developer = _mapper.Map<Developer>(developerEditVm);

            if (_developerService.Exist(developer))
            {
                ModelState.AddModelError(string.Empty, "This developer already exist.");

                developerEditVm.Cities = _cityService.GetDropDownListByCountry(developer.CountryId);
                developerEditVm.Countries = _countryService.GetDropDownList();

                return View(developerEditVm);
            }

            try
            {
                _developerService.Edit(developer);

                TempData["Success"] = "SUCCESS: Developer edited successfully.";
            }
            catch(Exception ex)
            {
                TempData["Error"] = $"ERROR: An error ocurred while trying to edit developer.\n{ex.Message}";
            }

            return RedirectToAction($"/Details/{developer.CityId}");
        }
    }
}