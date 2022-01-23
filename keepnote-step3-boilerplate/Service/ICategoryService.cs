using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace Service
{
    /*
	 * Should not modify this interface. You have to implement these methods in
	 * corresponding Impl classes
	 */
    public interface ICategoryService
    {
        Category CreateCategory(Category category);
        bool UpdateCategory(int categoryId,Category category);
        bool DeleteCategory(int categoryId);
        Category GetCategoryById(int categoryId);
        List<Category> GetAllCategoriesByUserId(string userId);
    }
}
