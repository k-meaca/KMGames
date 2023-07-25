using KMGames.Data.Interfaces;
using KMGames.Entities.DTOs.Category;
using KMGames.Entities.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KMGames.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        //----------PROPERTIES----------//

        private readonly KmGamesDBContext _dbContext;

        //----------CONSTRUCTOR----------//

        public CategoryRepository(KmGamesDBContext dBContext)
        {
            _dbContext = dBContext;
        }
     
        //----------METHODS----------//

        public void Add(Category category)
        {
            _dbContext.Categories.Add(category);
        }

        public void Delete(Category category)
        {
            _dbContext.Categories.Remove(category);
        }

        public void Edit(Category category)
        {
            _dbContext.Entry(category).State = System.Data.Entity.EntityState.Modified;
        }

        public bool Exist(Category category)
        {
            return _dbContext.Categories.Any(c => c.Name == category.Name);
        }

        public ICollection<CategoryListDto> GetCategories()
        {
            return _dbContext.Categories
                              .Select(c => new CategoryListDto()
                              {
                                  CategoryId = c.CategoryId,
                                  Name = c.Name,
                                  RowVersion = c.RowVersion
                              })
                              .ToList();
        }

        public List<CategoryListDto> GetCategoriesFrom(int gameId)
        {
            return _dbContext.GameCategories.Include(gc => gc.Category)
                                            .Where(gc => gc.GameId == gameId)
                                            .Select(gc => new CategoryListDto()
                                            {
                                                CategoryId = gc.CategoryId,
                                                Name = gc.Category.Name,
                                                RowVersion = gc.Category.RowVersion
                                            }).ToList();
        }

        public Category GetCategory(int id)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public Category GetCategory(string filter)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.Name == filter);
        }

        public List<CategoryCheckDto> GetCheckBoxList()
        {
            return _dbContext.Categories
                  .Select(c => new CategoryCheckDto()
                  {
                      CategoryId = c.CategoryId,
                      Category = c.Name,
                      Selected = false
                  })
                  .ToList();
        }

        public bool ItsRelated(Category category)
        {
            return _dbContext.GameCategories.Any(gm => gm.CategoryId == category.CategoryId);
        }
    }
}
