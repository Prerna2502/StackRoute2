using AuthenticationService.Exceptions;
using AuthenticationService.Models;
using AuthenticationService.Repository;
using System;

namespace AuthenticationService.Service
{
    public class AuthService : IAuthService
    {
        //define a private variable to represent repository
        private readonly IAuthRepository repository;
        //Use constructor Injection to inject all required dependencies.

        public AuthService(IAuthRepository authRepository)
        {
            repository = authRepository;
        }

        //This methos should be used to register a new user
        public bool RegisterUser(User user)
        {
            if (!repository.IsUserExists(user.UserId))
            {
                return repository.CreateUser(user);
            }
            throw new UserAlreadyExistsException($"This userId {user.UserId} already in use");
        }

        //This method should be used to login for existing user
        public bool LoginUser(User user)
        {
            if (repository.LoginUser(user))
                return true ;
            throw new Exception("Invalid user id or password");
        }
    }
}
