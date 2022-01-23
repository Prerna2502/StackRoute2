using Microsoft.AspNetCore.Mvc;
using Service;
using Entities;
using System;
using Exceptions;
using Newtonsoft.Json;

namespace KeepNote.Controllers
{
    /*
 * As in this assignment, we are working with creating RESTful web service, hence annotate
 * the class with [ApiController] annotation and define the controller level route as per REST Api standard.
 */
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        /*
      * CategoryService should  be injected through constructor injection. Please note that we should not create service
      * object using the new keyword
      */
        private readonly ICategoryService service;

        public CategoryController(ICategoryService categoryService)
        {
            service = categoryService;
        }

        /*
         * Define a handler method which will create a category by reading the
         * Serialized category object from request body and save the category in
         * category table in database. Please note that the careatorId has to be unique
         * and the userID should be taken as the categoryCreatedBy for the
         * category. This handler method should return any one of the status messages
         * basis on different situations: 1. 201(CREATED - In case of successful
         * creation of the category 2. 409(CONFLICT) - In case of duplicate categoryId
         * 
         *  * This handler method should map to the URL "/api/category" using HTTP POST method*/

        [HttpPost]
        public IActionResult Post(Category category)
        {
            try
            {
                Category c = service.CreateCategory(category);
                return StatusCode(201, c);
            }
            catch(Exception e)
            {
                return StatusCode(409, e.Message);
            }
        }

        /*
         * Define a handler method which will delete a category from a database.
         * 
         * This handler method should return any one of the status messages basis on
         * different situations: 1. 200(OK) - If the category deleted successfully from
         * database. 2. 404(NOT FOUND) - If the category with specified categoryId is
         * not found. 
         * 
         * This handler method should map to the URL "/api/category/{id}" using HTTP Delete
         * method" where "id" should be replaced by a valid categoryId without {}
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = service.DeleteCategory(id);
                return StatusCode(200, result);
            }
            catch(CategoryNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        /*
         * Define a handler method which will update a specific category by reading the
         * Serialized object from request body and save the updated category details in
         * a category table in database handle CategoryNotFoundException as well. please
         * note that the loggedIn userID should be taken as the categoryCreatedBy for
         * the category. This handler method should return any one of the status
         * messages basis on different situations: 1. 200(OK) - If the category updated
         * successfully. 2. 404(NOT FOUND) - If the category with specified categoryId
         * is not found. 
         * 
         * This handler method should map to the URL "/api/category/{id}" using HTTP PUT
         * method.
         */
        [HttpPut("{id}")]
        public IActionResult Put(int id, Category category)
        {
            try
            {
                bool result = service.UpdateCategory(id, category);
                return StatusCode(200, result);
            }
            catch(CategoryNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        /*
         * Define a handler method which will get us the category by a userId.
         * 
         * This handler method should return any one of the status messages basis on
         * different situations: 1. 200(OK) - If the category found successfully. 
         * 
         * This handler method should map to the URL "/api/category/{userId}" using HTTP GET method
         */
        [HttpGet("{userId}")]
        public IActionResult Get(string userId)
        {
            return StatusCode(200, service.GetAllCategoriesByUserId(userId));
        }

        [HttpGet("{categoryId:int}")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            try
            {
                Category category = service.GetCategoryById(categoryId);
                return StatusCode(200, category);
            }
            catch(CategoryNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}
