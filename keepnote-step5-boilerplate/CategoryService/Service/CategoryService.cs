using System;
using System.Collections.Generic;
using CategoryService.Models;
using CategoryService.Repository;
using CategoryService.Exceptions;
using MongoDB.Driver;
using System.Linq;

namespace CategoryService.Service
{
    public class CategoryService:ICategoryService
    {
        //define a private variable to represent repository
        private readonly ICategoryRepository _repository;
        //Use constructor Injection to inject all required dependencies.
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        //This method should be used to save a new category.
        public Category CreateCategory(Category category)
        {
            List<Category> categories = _repository.GetAllCategoriesByUserId(category.CreatedBy);
            int flag = 0;
            foreach(Category c in categories)
            {
                if(c.Id == category.Id)
                {
                    flag = 1;
                }
            }
            if (flag == 0)
            {
                return _repository.CreateCategory(category);
            }
            throw new CategoryNotCreatedException("This category already exists");
        }
        //This method should be used to delete an existing category.
        public bool DeleteCategory(int categoryId)
        {
            if (_repository.DeleteCategory(categoryId))
            {
                return true;
            }
            throw new CategoryNotFoundException("This category id not found");
        }
        // This method should be used to get all category by userId
        public List<Category> GetAllCategoriesByUserId(string userId)
        {
            return _repository.GetAllCategoriesByUserId(userId);
        }
        //This method should be used to get a category by categoryId.
        public Category GetCategoryById(int categoryId)
        {
            Category category1 = _repository.GetCategoryById(categoryId);
            if (category1 != null)
            {
                return _repository.GetCategoryById(categoryId);
            }
            throw new CategoryNotFoundException("This category id not found");
        }
        //This method should be used to update an existing category.
        public bool UpdateCategory(int categoryId, Category category)
        {
            Category category1 = _repository.GetCategoryById(categoryId);
            if (category1 != null)
            {
                return _repository.UpdateCategory(categoryId,category);
            }
            throw new CategoryNotFoundException("This category id not found");
        }
    }
}
