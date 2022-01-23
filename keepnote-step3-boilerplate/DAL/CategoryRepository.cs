using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace DAL
{
    //Repository class is used to implement all Data access operations
    public class CategoryRepository : ICategoryRepository
    {
        private readonly KeepDbContext db;
        public CategoryRepository(KeepDbContext dbContext)
        {
            db = dbContext;
        }
        /*
	    * This method should be used to save a new category.
	    */
        public Category CreateCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return db.Categories.Where(c => c.CategoryId == category.CategoryId).FirstOrDefault();
        }
        /* This method should be used to delete an existing category. */
        public bool DeleteCategory(int categoryId)
        {
            var category = db.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();
            if(category == null)
            {
                return false;
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            var catFound = db.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();
            if(catFound == null)
            {
                return true;
            }
            return false;
        }
        //* This method should be used to get all category by userId.
        public List<Category> GetAllCategoriesByUserId(string userId)
        {
            return db.Categories.ToList();
        }

        /*
	     * This method should be used to get a category by categoryId.
	     */
        public Category GetCategoryById(int categoryId)
        {
            return db.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();
        }

        /*
	    * This method should be used to update a existing category.
	    */
        public bool UpdateCategory(Category category)
        {
            var catg = db.Categories.Where(c => c.CategoryId == category.CategoryId).FirstOrDefault();
            catg.CategoryName = category.CategoryName;
            catg.CategoryDescription = category.CategoryDescription;
            catg.CategoryCreatedBy = category.CategoryCreatedBy;
            catg.CategoryCreationDate = category.CategoryCreationDate;
            db.Entry<Category>(catg).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return true;
        }
    }
}
