using System;
using UserService.Exceptions;
using UserService.Models;
using UserService.Repository;

namespace UserService.Service
{
    public class UserService : IUserService
    {
        //define a private variable to represent repository

        private readonly IUserRepository _repository;

        //Use constructor Injection to inject all required dependencies.

        public UserService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        //This method should be used to delete an existing user.
        public bool DeleteUser(string userId)
        {
            User user = _repository.GetUserById(userId);
            if (user != null)
            {
                return _repository.DeleteUser(userId);
            }
            throw new UserNotFoundException("This user id does not exist");
        }
        //This method should be used to delete an existing user
        public User GetUserById(string userId)
        {
            User user = _repository.GetUserById(userId);
            if (user != null)
            {
                return user;
            }
            throw new UserNotFoundException("This user id does not exist");
        }
        //This method is used to register a new user
        public User RegisterUser(User user)
        {
            User userFound = _repository.GetUserById(user.UserId);
            if (userFound == null)
            {
                return _repository.RegisterUser(user);
            }
            throw new UserNotCreatedException("This user id already exists");
        }
        //This methos is used to update an existing user
        public bool UpdateUser(string userId, User user)
        {
            User userFound = _repository.GetUserById(user.UserId);
            if (userFound != null)
            {
                return _repository.UpdateUser(userId, user);
            }
            throw new UserNotFoundException("This user id does not exist");
        }
    }
}
