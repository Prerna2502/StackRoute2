using System;
using System.Collections.Generic;
using System.Linq;
using CategoryService.Models;
using MongoDB.Driver;

namespace CategoryService.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        //define a private variable to represent CategoryContext
        private readonly CategoryContext context;

        public CategoryRepository(CategoryContext _context)
        {
            context = _context;
        }

        //This method should be used to save a new category.
        public Category CreateCategory(Category category)
        {
            var sd = context.Categories.Find(x => true).SortByDescending(d => d.Id).Limit(1).FirstOrDefaultAsync();
            category.Id = sd.Result.Id + 1;

            context.Categories.InsertOne(category);
            return context.Categories.Find(x => x.Id == category.Id).FirstOrDefault();
        }

        //This method should be used to delete an existing category.
        public bool DeleteCategory(int categoryId)
        {
            context.Categories.DeleteOne(x => x.Id == categoryId);
            return true;
        }

        //This method should be used to get all category by userId
        public List<Category> GetAllCategoriesByUserId(string userId)
        {
            return context.Categories.Find(x => x.CreatedBy == userId).ToList();
        }

        //This method should be used to get a category by categoryId
        public Category GetCategoryById(int categoryId)
        {
            return context.Categories.Find(x => x.Id == categoryId).FirstOrDefault();
        }

        // This method should be used to update an existing category.
        public bool UpdateCategory(int categoryId, Category category)
        {
            var filter = Builders<Category>.Filter.Where(x => x.Id == categoryId);
            var update = Builders<Category>.Update.Set(x => x.Name, category.Name)
                .Set(x => x.Description, category.Description)
                .Set(x => x.CreationDate, category.CreationDate)
                .Set(x => x.CreatedBy, category.CreatedBy);
            context.Categories.UpdateOne(filter, update);
            return true;
        }
    }
}
