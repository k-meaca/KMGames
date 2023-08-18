using AutoMapper;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using KMGames.Web.App_Start;
using KMGames.Web.Helpers;
using KMGames.Web.ViewModel.Cities;
using KMGames.Web.ViewModel.Games;
using KMGames.Web.ViewModel.Users;
using Microsoft.Owin.Security.Provider;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.Controllers
{
    public class UsersController : Controller
    {
        //----------PROPERTIES----------//

        private readonly IUserService _userService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        private readonly IMapper _mapper;

        //----------CONSTRUCTOR----------/

        public UsersController(IUserService userService, ICountryService countryService, ICityService cityService)
        {
            _userService = userService;
            _countryService = countryService;
            _cityService = cityService;

            _mapper = AutoMapperConfig.Mapper;
        }

        //----------METHODS----------//
        
        public JsonResult GetCities(int countryId)
        {
            var cities = _cityService.GetCities(countryId);

            var citiesListVm = _mapper.Map<List<CityListVm>>(cities);

            return Json(citiesListVm);
        }

        // GET: Users
        public ActionResult Index(int? page, int? pageSize)
        {
            var users = _userService.GetUsers();

            var userListVm = _mapper.Map<List<UserListVm>>(users);

            page = page ?? 1;
            pageSize = pageSize ?? 10;

            ViewBag.PageSize = pageSize;

            return View(userListVm.ToPagedList(page.Value,pageSize.Value));
        }

        public ActionResult Create()
        {
            var userEditVm = new UserEditVm()
            {
                Countries = _countryService.GetDropDownList(),
                Cities = new List<SelectListItem>()
            };

            return View(userEditVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserEditVm userEditVm)
        {
            if (!ModelState.IsValid)
            {
                userEditVm.Countries = _countryService.GetDropDownList();

                return View(userEditVm);
            }

            var user = _mapper.Map<User>(userEditVm);

            if (_userService.Exist(user))
            {
                ModelState.AddModelError(string.Empty, "There alredy exist a user with that name or email.");
                
                userEditVm.Countries = _countryService.GetDropDownList();

                userEditVm.Cities = _cityService.GetDropDownListByCountry(userEditVm.CountryId);

                return View(userEditVm);
            }

            try
            {
                using(var transaction = new TransactionScope())
                {
                    user.CreationDate = DateTime.Now;

                    _userService.Add(user);

                    UsersHelper.CreateUserAsp(user.Email, "User");

                    TempData["Success"] = "SUCCESS: User added successfully.";

                    transaction.Complete();
                }
            }
            catch(Exception ex)
            {
                TempData["Error"] = $"An error ocurred while trying to add the user.\n{ex.Message}";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if(id is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var user = _userService.GetUser(id);

            if(user is null)
            {
                TempData["Error"] = "This user/customer was deleted by other administrator.";

                return RedirectToAction("Index");
            }
            else if (_userService.ItsRelated(id))
            {
                TempData["Error"] = "This user is related to some sales. Can not be deleted.";

                return RedirectToAction("Index");
            }

            var gameListVm = _mapper.Map<UserListVm>(user);

            return View(gameListVm);
        }


        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(string id)
        {
            var user = _userService.GetUser(id);

            _userService.Delete(user);

            TempData["Warning"] = $"WARNING: {user.Email} was deleted from database.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            if (id is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var user = _userService.GetUser(id);

           
            if (user is null)
            {
                TempData["Error"] = "This user/customer was deleted by other administrator.";

                return RedirectToAction("Index");
            }

            var userEditVm = _mapper.Map<UserEditVm>(user);

            userEditVm.DeprecatedEmail = user.Email;

            userEditVm.Countries = _countryService.GetDropDownList();

            userEditVm.Cities = _cityService.GetDropDownListByCountry(userEditVm.CountryId);

            return View(userEditVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditVm userEditVm)
        {
            if (!ModelState.IsValid)
            {
                userEditVm.Countries = _countryService.GetDropDownList();

                return View(userEditVm);
            }

            var user = _mapper.Map<User>(userEditVm);

            if (_userService.Exist(user))
            {
                ModelState.AddModelError(string.Empty, "There already exist user with that nickname or email.");

                userEditVm.Countries = _countryService.GetDropDownList();

                return View(userEditVm);
            }

            try
            {
                using (var transaction = new TransactionScope())
                {
                    _userService.Edit(user);

                    UsersHelper.UpdateUserName(userEditVm.DeprecatedEmail, user.Email);
                    
                    TempData["Success"] = "SUCCESS: User edited successfully";

                    transaction.Complete();
                }
            }
            catch(Exception ex)
            {
                TempData["Error"] = $"An error ocurred while trying to edit the user.\n{ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}