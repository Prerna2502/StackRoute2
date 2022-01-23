using DAL;
using Entities;
using Exceptions;
using System;

namespace Service
{
        /*
      * Service classes are used here to implement additional business logic/validation
      * */
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        /*
       Use constructor Injection to inject all required dependencies.
       */
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        //This method should be used to delete an existing user. 
        public bool DeleteUser(string userId)
        {
            User user = _repository.GetUserById(userId);
            if (user != null)
            {
                return _repository.DeleteUser(userId);
            }
            throw new UserNotFoundException($"User with id: {userId} does not exist");
        }

        //This method should be used to get a user by userId.
        public User GetUserById(string userId)
        {
            User user = _repository.GetUserById(userId);
            if (user != null)
            {
                return user;
            }
            throw new UserNotFoundException($"User with id: {userId} does not exist");
        }

        //This method should be used to save a new user.
        public bool RegisterUser(User user)
        {
            User userFound = _repository.GetUserById(user.UserId);
            if (userFound == null)
            {
                return _repository.RegisterUser(user);
            }
            throw new UserAlreadyExistException($"This userid: {user.UserId} already exists");
        }

        //This method should be used to update an existing user.
        public bool UpdateUser(string userId,User user)
        {
            User userFound = _repository.GetUserById(user.UserId);
            if (userFound != null)
            {
                return _repository.UpdateUser(user);
            }
            throw new UserNotFoundException($"User with id: {userId} does not exist");
        }

        //This method should be used to validate a user using userId and password.
        public bool ValidateUser(string userId, string password)
        {
            User userFound = _repository.GetUserById(userId);
            if (userFound != null)
            {
                return _repository.ValidateUser(userId, password);
            }
            throw new UserNotFoundException($"User with id: {userId} does not exist");
        }
    }
}
