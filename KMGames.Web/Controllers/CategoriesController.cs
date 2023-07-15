using AutoMapper;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using KMGames.Web.App_Start;
using KMGames.Web.ViewModel.Categories;
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
    public class CategoriesController : Controller
    {
        //----------PROPERTIES----------//

        private readonly ICategoryService _categoryService;

        private readonly IMapper _mapper;

        //----------CONSTRUCTOR----------//

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _mapper = AutoMapperConfig.Mapper;
        }

        //----------ACTIONS----------//

        // GET: Categories
        public ActionResult Index(int? page, int? pageSize)
        {
            var categoryList = _categoryService.GetCategories();

            var categoryListVm = _mapper.Map<List<CategoryListVm>>(categoryList);

            page = page ?? 1;
            pageSize = pageSize ?? 10;

            ViewBag.PageSize = pageSize;

            return View(categoryListVm.ToPagedList(page.Value,pageSize.Value));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryEditVm categoryEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryEditVm);
            }

            var category = _mapper.Map<Category>(categoryEditVm);

            if (_categoryService.Exist(category))
            {
                ModelState.AddModelError(string.Empty, $"The category: {category.Name} already exists.");

                return View(categoryEditVm);
            }

            _categoryService.Add(category);

            TempData["Success"] = "SUCCESS: Category added successfully";

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _categoryService.GetCategory(id.Value);

            if(category is null)
            {
                TempData["Error"] = "ERROR: This category was deleted by other user.";

                return RedirectToAction("Index");
            }
            else if (_categoryService.ItsRelated(category))
            {
                TempData["Error"] = $"ERROR: {category.Name} is related to other data. It can't be deleted.";

                return RedirectToAction("Index");
            }
            else
            {
                var categoryListVm = _mapper.Map<CategoryListVm>(category);

                return View(categoryListVm);
            }
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = _categoryService.GetCategory(id);

            _categoryService.Delete(category);

            TempData["Warning"] = "WARNING: Category deleted successfully.";

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
        {
            if(id is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _categoryService.GetCategory(id.Value);

            if(category is null)
            {
                TempData["Error"] = "ERROR: This category was deleted by other user.";

                return RedirectToAction("Index");
            }

            var categoryEditVm = _mapper.Map<CategoryEditVm>(category);

            return View(categoryEditVm);

        }

        [HttpPost]
        public ActionResult Edit(CategoryEditVm categoryEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryEditVm);
            }

            var category = _mapper.Map<Category>(categoryEditVm);

            if (_categoryService.Exist(category))
            {
                ModelState.AddModelError(string.Empty, "There already exist a category with that name.");

                return View(categoryEditVm);
            }

            _categoryService.Edit(category);

            TempData["Success"] = "SUCCESS: Category edited successfully.";

            return RedirectToAction("Index");
        }

    }
}