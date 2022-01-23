using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Entities;
using Exceptions;

namespace Service
{
    /*
    * Service classes are used here to implement additional business logic/validation
    * */
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository repository;
        /*
        Use constructor Injection to inject all required dependencies.
            */
        public CategoryService(ICategoryRepository categoryRepository)
        {
            repository = categoryRepository;
        }

        /*
	    * This method should be used to save a new category.
	    */
        public Category CreateCategory(Category category)
        {
            Category cat = repository.GetCategoryById(category.CategoryId);
            if (cat == null)
            {
                return repository.CreateCategory(category);
            }
            throw new Exception($"Category with category id: {category.CategoryId} already exists");

        }

        /* This method should be used to delete an existing category. */
        public bool DeleteCategory(int categoryId)
        {
            bool result = repository.DeleteCategory(categoryId);
            if (result)
            {
                return result;
            }
            throw new CategoryNotFoundException($"Category with id: {categoryId} does not exist");
        }

        /*
	     * This method should be used to get all category by userId.
	     */
        public List<Category> GetAllCategoriesByUserId(string userId)
        {
            return repository.GetAllCategoriesByUserId(userId);
        }

        /*
	     * This method should be used to get a category by categoryId.
	     */
        public Category GetCategoryById(int categoryId)
        {
            Category cat = repository.GetCategoryById(categoryId);
            if (cat != null)
            {
                return cat;
            }
            throw new CategoryNotFoundException($"Category with id: {categoryId} does not exist");
        }

        /*
	    * This method should be used to update a existing category.
	    */
        public bool UpdateCategory(int categoryId, Category category)
        {
            Category cat = repository.GetCategoryById(categoryId);
            if (cat != null)
            {
                return repository.UpdateCategory(category);
            }
            throw new CategoryNotFoundException($"Category with id: {categoryId} does not exist");
        }
    }
}
