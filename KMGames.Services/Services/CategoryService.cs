using KMGames.Data;
using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.Category;
using KMGames.Entities.Entities;
using KMGames.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KMGames.Services.Services
{
    [Authorize]
    public class CategoryService : ICategoryService
    {
        //----------PROPERTIES----------//

        private readonly IUnitOfWork _unitOfWork;

        private readonly ICategoryRepository _categoryRepository;

        //----------CONSTRUCTOR----------//

        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        //----------METHODS----------//
        
        public void Add(Category category)
        {
            _categoryRepository.Add(category);
            _unitOfWork.SaveChanges();
        }

        public void Delete(Category category)
        {
            _categoryRepository.Delete(category);
            _unitOfWork.SaveChanges();
        }

        public void Edit(Category category)
        {
            _categoryRepository.Edit(category);
            _unitOfWork.SaveChanges();
        }

        public bool Exist(Category category)
        {
            return _categoryRepository.Exist(category);
        }

        public ICollection<CategoryListDto> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public List<CategoryListDto> GetCategoriesFrom(int gameId)
        {
            return _categoryRepository.GetCategoriesFrom(gameId);
        }

        public Category GetCategory(int id)
        {
            return _categoryRepository.GetCategory(id);
        }

        public Category GetCategory(string filter)
        {
            return _categoryRepository.GetCategory(filter);
        }

        public List<CategoryCheckDto> GetCheckBoxList()
        {
            return _categoryRepository.GetCheckBoxList();
        }

        public bool ItsRelated(Category category)
        {
            return _categoryRepository.ItsRelated(category);
        }
    }
}
