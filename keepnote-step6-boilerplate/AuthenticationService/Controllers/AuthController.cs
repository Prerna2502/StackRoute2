using AuthenticationService.Service;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService.Models;
using AuthenticationService.Exceptions;
using System;

namespace AuthenticationService.Controllers
{
    /*
   As in this assignment, we are working with creating RESTful web service to create microservices, hence annotate
   the class with [ApiController] annotation and define the controller level route as per REST Api standard.
   */
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        /*
       AuthService should  be injected through constructor injection. Please note that we should not create service
       object using the new keyword
      */
        private readonly IAuthService service;
        private readonly ITokenGeneratorService tokenService;
        public AuthController(IAuthService authService, ITokenGeneratorService tokenGeneratorService)
        {
            service = authService;
            tokenService = tokenGeneratorService;
        }

        /*
	     * Define a handler method which will create a specific user by reading the
	     * Serialized object from request body and save the user details in the
	     * database. This handler method should return any one of the status messages
	     * basis on different situations:
	     * 1. 201(CREATED) - If the user created successfully. 
	     * 2. 409(CONFLICT) - If the userId conflicts with any existing user
	     * 
	     * This handler method should map to the URL "/api/auth/register" using HTTP POST method
    	 */
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            try
            {
                var result = service.RegisterUser(user);
                return StatusCode(201, result);
            }
            catch(UserAlreadyExistsException e)
            {
                return StatusCode(409, e.Message);
            }
        }



        /* Define a handler method which will authenticate a user by reading the Serialized user
         * object from request body containing the username and password. The username and password should be validated 
         * before proceeding ahead with JWT token generation. The user credentials will be validated against the database entries. 
         * The error should be return if validation is not successful. If credentials are validated successfully, then JWT
         * token will be generated. The token should be returned back to the caller along with the API response.
         * This handler method should return any one of the status messages basis on different
         * situations:
         * 1. 200(OK) - If login is successful
         * 2. 401(UNAUTHORIZED) - If login is not successful
         * 
         * This handler method should map to the URL "/api/auth/login" using HTTP POST method
        */
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                service.LoginUser(user);

                return StatusCode(200, tokenService.GenerateJWTToken(user.UserId));
            }
            catch (Exception e)
            {
                return StatusCode(401, e.Message);
            }
        }
    }
}
