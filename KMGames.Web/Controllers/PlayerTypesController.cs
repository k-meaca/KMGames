using AutoMapper;
using KMGames.Data;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using KMGames.Web.App_Start;
using KMGames.Web.ViewModel.PlayerType;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;

namespace KMGames.Web.Controllers
{
    [Authorize]
    public class PlayerTypesController : Controller
    {
        //----------PROPERTIES----------//

        private readonly IPLayerTypeService _playerTypeService;

        private readonly IMapper _mapper;

        //----------CONSTRUCTOR----------//

        public PlayerTypesController(IPLayerTypeService playerTypeService)
        {
            _playerTypeService = playerTypeService;

            _mapper = AutoMapperConfig.Mapper;
        }

        //----------ACTIONS----------//

        // GET: PlayerType
        public ActionResult Index(int? page, int? pageSize)
        {
            var typesList = _playerTypeService.GetPlayerTypes();

            var typesListVm = _mapper.Map<List<PlayerTypeListVm>>(typesList);

            page = page ?? 1;
            pageSize = pageSize ?? 10;

            ViewBag.PageSize = pageSize;

            return View(typesListVm.ToPagedList(page.Value,pageSize.Value));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PlayerTypeEditVm typeEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(typeEditVm);
            }

            var playerType = _mapper.Map<PlayerType>(typeEditVm);

            if (_playerTypeService.Exist(playerType))
            {
                ModelState.AddModelError(string.Empty, "This player type already exist.");

                return View(typeEditVm);
            }

            _playerTypeService.Add(playerType);

            TempData["Success"] = "SUCCESS: Player type added successfully.";

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? id)
        {
            if(id is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var playerType = _playerTypeService.GetPlayerType(id.Value);

            if(playerType is null)
            {
                TempData["Error"] = "ERROR: This player type was deleted by other user.";

                return RedirectToAction("Index");
            }
            else if (_playerTypeService.ItsRelated(id.Value))
            {
                TempData["Error"] = "ERROR: This player type is related to some games.";

                return RedirectToAction("Index");
            }

            var typeListVm = _mapper.Map<PlayerTypeListVm>(playerType);

            return View(typeListVm);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var playerType = _playerTypeService.GetPlayerType(id);

            _playerTypeService.Delete(playerType);

            TempData["Warning"] = $"WARNING: {playerType.Type} deleted sucessfully";

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
        {
            if (id is null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var playerType = _playerTypeService.GetPlayerType(id.Value);

            if (playerType is null)
            {
                TempData["Error"] = "ERROR: This player type was deleted by other user.";

                return RedirectToAction("Index");
            }

            var typeEditVm = _mapper.Map<PlayerTypeEditVm>(playerType);

            return View(typeEditVm);
        }

        [HttpPost]
        public ActionResult Edit(PlayerTypeEditVm typeEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(typeEditVm);
            }

            var playerType = _mapper.Map<PlayerType>(typeEditVm);

            if (_playerTypeService.Exist(playerType))
            {
                ModelState.AddModelError(string.Empty, "This player type already exist.");

                return View(typeEditVm);
            }

            _playerTypeService.Edit(playerType);

            TempData["Success"] = "SUCCESS: Player type edited successfully.";

            return RedirectToAction("Index");
        }


    }
}