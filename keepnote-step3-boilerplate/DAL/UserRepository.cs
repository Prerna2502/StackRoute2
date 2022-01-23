using System;
using System.Linq;
using Entities;

namespace DAL
{
    //Repository class is used to implement all Data access operations
    public class UserRepository : IUserRepository
    {
        private readonly KeepDbContext db;
        public UserRepository(KeepDbContext dbContext)
        {
            db = dbContext;
        }

        //This method should be used to delete an existing user. 
        public bool DeleteUser(string userId)
        {
            var user = db.Users.Where(u => u.UserId == userId).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
            var userFound = db.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if (userFound == null)
            {
                return true;
            }
            return false;
        }
        //This method should be used to get a user by userId.
        public User GetUserById(string userId)
        {
            return db.Users.Where(u => u.UserId == userId).FirstOrDefault();
        }
        //This method should be used to save a new user.
        public bool RegisterUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            var userAdded = db.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();
            if(userAdded != null)
            {
                return true;
            }
            return false;
        }
        //This method should be used to update an existing user.
        public bool UpdateUser(User user)
        {
            var userToUpdate = db.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();
            userToUpdate.UserName = user.UserName;
            userToUpdate.Password = user.Password;
            userToUpdate.Contact = user.Contact;
            db.Entry<User>(userToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return true;
        }
        //This method should be used to validate a user using userId and password.
        public bool ValidateUser(string userId, string password)
        {
            var user = db.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if(user.Password == password)
            {
                return true;
            }
            return false;
        }
    }
}
