using AutoMapper;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using KMGames.Web.App_Start;
using KMGames.Web.ViewModel.Cities;
using KMGames.Web.ViewModel.Country;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.Controllers
{
    [Authorize]
    public class CountriesController : Controller
    {
        //----------PROPERTIES-----------//

        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        //----------CONTRUCTOR----------//

        public CountriesController(ICountryService countryService, ICityService cityService)
        {
            _countryService = countryService;
            _cityService = cityService;
            _mapper = AutoMapperConfig.Mapper;
        }

        // GET: Countries
        public ActionResult Index(int? page, int? pageSize)
        {
            var countriesList = _countryService.GetCountries();

            var listVm = _mapper.Map<List<CountryListVm>>(countriesList);

            page = page ?? 1;
            pageSize = pageSize ?? 10;

            ViewBag.PageSize = pageSize;

            return View(listVm.ToPagedList(page.Value,pageSize.Value));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryEditVm countryEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(countryEditVm);
            }

            var country = _mapper.Map<Country>(countryEditVm);

            if (_countryService.Exist(country))
            {
                ModelState.AddModelError(string.Empty,$"There already exists a country with {country.Name} name.");
                return View(countryEditVm);
            }

            _countryService.Add(country);

            TempData["Success"] = "SUCCESS: Country saved successfully";

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if(id is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var country = _countryService.GetCountry(id.Value);

            if(country is null)
            {
                TempData["Error"] = "ERROR: The country was deleted by other user.";

                return RedirectToAction("Index");
            }
            else if (_countryService.ItsRelated(country))
            {
                TempData["Error"] = $"ERROR: {country.Name} it's related to some data. It can't be deleted";

                return RedirectToAction("Index");
            }
            else
            {
                var countryListVm = _mapper.Map<CountryListVm>(country);

                return View(countryListVm);
            }
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var country = _countryService.GetCountry(id);

            _countryService.Delete(country);

            TempData["Warning"] = $"WARNING: {country.Name} removed successfully.";

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
        {
            if(id is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var country = _countryService.GetCountry(id.Value);

            if(country is null)
            {
                TempData["Error"] = "ERROR: The country was deleted by other user.";

                return RedirectToAction("Index");
            }

            var countryEditVm = _mapper.Map<CountryEditVm>(country);

            return View(countryEditVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CountryEditVm countryEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(countryEditVm);
            }

            var country = _mapper.Map<Country>(countryEditVm);

            if (_countryService.Exist(country))
            {
                ModelState.AddModelError(string.Empty, "There alredy exist a country with that name.");

                return View(countryEditVm);
            }

            _countryService.Edit(country);

            TempData["Success"] = "SUCCESS: Country edited successfully";

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if(id is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var country = _countryService.GetCountry(id.Value);

            if (country is null)
            {
                TempData["Error"] = "ERROR: The country was deleted by other user.";

                return RedirectToAction("Index");
            }

            var countryListVm = _mapper.Map<CountryListVm>(country);

            var cityListVm = _mapper.Map<List<CityListVm>>(_cityService.GetCities(country.CountryId));

            var countryDetails = new CountryDetailsVm()
            {
                Country = countryListVm,
                Cities = cityListVm
            };

            return View(countryDetails);
        }

        public ActionResult AddCity(int? countryId)
        {
            if (countryId is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var country = _countryService.GetCountry(countryId.Value);

            if (country is null)
            {
                TempData["Error"] = "ERROR: The country was deleted by other user.";

                return RedirectToAction($"/Details/{countryId.Value}");
            }

            var cityEditVm = new CityEditVm()
            {
                CountryId = countryId.Value,
                Countries = _countryService.GetDropDownList()
            };

            return View(cityEditVm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCity(CityEditVm cityEditVm)
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

            try
            {
                _cityService.Add(city);

                TempData["Success"] = "SUCCESS: City added successfully";
            }
            catch(Exception ex)
            {
                TempData["Error"] = $"ERROR: An error ocurred while trying to add a new city.\n{ex.Message}";
            }
            
            return RedirectToAction($"/Details/{cityEditVm.CountryId}");
        }

        public ActionResult DeleteCity(int? cityId)
        {
            if (cityId is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var city = _cityService.GetCity(cityId.Value);

            if (city is null)
            {
                TempData["Error"] = "ERROR: The city was deleted by other user.";

                return RedirectToAction($"Index");
            }
            else if (_cityService.ItsRelated(cityId.Value))
            {
                TempData["Error"] = "ERROR: The city is related to some developers. It can't be deleted.";

                return RedirectToAction($"/Details/{city.CountryId}");
            }

            var cityEditVm = _mapper.Map<CityEditVm>(city);

            cityEditVm.Countries = _countryService.GetDropDownList();

            return View(cityEditVm);
        }

        [HttpPost,ActionName("DeleteCity")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCity(int cityId)
        {
            var city = _cityService.GetCity(cityId);

            _cityService.Delete(city);

            TempData["Warning"] = $"WARNING: {city.Name} was deleted from database.";

            return RedirectToAction($"/Details/{city.CountryId}");
        }

        public ActionResult EditCity(int? cityId)
        {
            if (cityId is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var city = _cityService.GetCity(cityId.Value);

            if (city is null)
            {
                TempData["Error"] = "ERROR: The city was deleted by other user.";

                return RedirectToAction($"/Details/{city.CountryId}");
            }

            var cityEditVm = _mapper.Map<CityEditVm>(city);

            cityEditVm.Countries = _countryService.GetDropDownList();

            return View(cityEditVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCity(CityEditVm cityEditVm)
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

            try
            {
                _cityService.Edit(city);

                TempData["Success"] = "SUCCESS: City edited successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"ERROR: An error ocurred while trying to edit the city.\n{ex.Message}";
            }

            return RedirectToAction($"/Details/{cityEditVm.CountryId}");
        }
    }
}