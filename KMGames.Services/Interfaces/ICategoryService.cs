using KMGames.Entities.DTOs.Category;
using KMGames.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KMGames.Services.Interfaces
{
    public interface ICategoryService
    {
        void Add(Category category);

        void Delete(Category category);

        void Edit(Category category);

        bool Exist(Category category);

        ICollection<CategoryListDto> GetCategories();
        
        List<CategoryListDto> GetCategoriesFrom(int gameId);
        
        Category GetCategory(int id);

        Category GetCategory(string filter);

        List<CategoryCheckDto> GetCheckBoxList();

        bool ItsRelated(Category category);

    }
}
