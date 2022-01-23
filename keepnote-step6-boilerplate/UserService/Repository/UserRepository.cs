using System;
using System.Linq;
using MongoDB.Driver;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly UserContext context;
        public UserRepository(UserContext _context)
        {
            context = _context;
        }
        //This method should be used to delete an existing user.
        public bool DeleteUser(string userId)
        {
            context.Users.DeleteOne(x => x.UserId == userId);
            return true;
        }

        //This method should be used to delete an existing user
        public User GetUserById(string userId)
        {
            return context.Users.Find(x => x.UserId == userId).FirstOrDefault();
        }
        //This method is used to register a new user
        public User RegisterUser(User user)
        {
            context.Users.InsertOne(user);
            return context.Users.Find(x => x.UserId == user.UserId).FirstOrDefault();
        }
        //This methos is used to update an existing user
        public bool UpdateUser(string userId, User user)
        {
            var filter = Builders<User>.Filter.Where(x => x.UserId == userId);
            var update = Builders<User>.Update.Set(x => x.Name, user.Name)
                .Set(x => x.Contact, user.Contact)
                .Set(x => x.AddedDate, user.AddedDate);
            context.Users.UpdateOne(filter, update);
            return true;
        }
    }
}
