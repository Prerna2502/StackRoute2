using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReminderService.Exceptions;
using ReminderService.Models;
using ReminderService.Service;

namespace ReminderService.Controllers
{
    /*
    As in this assignment, we are working with creating RESTful web service to create microservices, hence annotate
    the class with [ApiController] annotation and define the controller level route as per REST Api standard.
    */
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : Controller
    {
        /*
	 * From the problem statement, we can understand that the application requires
	 * us to implement five functionalities regarding reminder. They are as
	 * following:
	 * 
	 * 1. Create a reminder 
	 * 2. Delete a reminder 
	 * 3. Update a reminder 
	 * 4. Get all reminders by userId 
	 * 5. Get a specific reminder by id.
	 * 
	 */


        /*
     ReminderService should  be injected through constructor injection. Please note that we should not create service
     object using the new keyword
    */
        private readonly IReminderService service;

        public ReminderController(IReminderService reminderService)
        {
            service = reminderService;
        }

        /*
	 * Define a handler method which will create a reminder by reading the
	 * Serialized reminder object from request body and save the reminder in
	 * database. Please note that the reminderId has to be unique. This handler
	 * method should return any one of the status messages basis on different
	 * situations: 
	 * 1. 201(CREATED - In case of successful creation of the reminder
	 * 2. 409(CONFLICT) - In case of duplicate reminder ID
	 *
	 * This handler method should map to the URL "/api/reminder" using HTTP POST
	 * method".
	 */
        [HttpPost]
        public IActionResult Post(Reminder reminder)
        {
            try
            {
                Reminder result = service.CreateReminder(reminder);
                return StatusCode(201, result);
            }
            catch (Exception e)
            {
                return StatusCode(409, e.Message);
            }
        }

        /*
         * Define a handler method which will delete a reminder from a database.
         * 
         * This handler method should return any one of the status messages basis on
         * different situations: 
         * 1. 200(OK) - If the reminder deleted successfully from database. 
         * 2. 404(NOT FOUND) - If the reminder with specified reminderId is not found.
         * 
         * This handler method should map to the URL "/api/reminder/{id}" using HTTP Delete
         * method" where "id" should be replaced by a valid reminderId without {}
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Reminder reminder = service.GetReminderById(id);
                if (reminder == null)
                {
                    throw new ReminderNotFoundException("This reminder id not found");
                }
                bool result = service.DeleteReminder(id);
                return StatusCode(200, result);
            }
            catch (ReminderNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        /*
         * Define a handler method which will update a specific reminder by reading the
         * Serialized object from request body and save the updated reminder details in
         * a database. This handler method should return any one of the status messages
         * basis on different situations: 
         * 1. 200(OK) - If the reminder updated successfully. 
         * 2. 404(NOT FOUND) - If the reminder with specified reminderId is not found. 
         * 
         * This handler method should map to the URL "/api/reminder/{id}" using HTTP PUT
         * method.
         */
        [HttpPut("{id}")]
        public IActionResult Put(int id, Reminder reminder)
        {
            try
            {
                return StatusCode(200, service.UpdateReminder(id, reminder));
            }
            catch (ReminderNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        /*
         * Define a handler method which will show details of a specific reminder. This
         * handler method should return any one of the status messages basis on
         * different situations: 
         * 1. 200(OK) - If the reminder found successfully. 
         * 2. 404(NOT FOUND) - If the reminder with specified reminderId is not found. 
         * 
         * This handler method should map to the URL "/api/reminder/{id}" using HTTP GET method
         * where "id" should be replaced by a valid reminderId without {}
         */
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                Reminder reminder = service.GetReminderById(id);
                return StatusCode(200, reminder);
            }
            catch (ReminderNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }



        /*
         * Define a handler method which will get us the all reminders.
         * This handler method should return any one of the status messages basis on
         * different situations: 
         * 1. 200(OK) - If the reminder found successfully. 
         * 2. 404(NOT FOUND) - If the reminder with specified reminderId is not found.
         * 
         * This handler method should map to the URL "/api/reminder" using HTTP GET method
         */
        [HttpGet("{userId}")]
        public IActionResult Get(string userId)
        {
            try
            {
                return StatusCode(200, service.GetAllRemindersByUserId(userId));
            }
            catch (ReminderNotFoundException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}
