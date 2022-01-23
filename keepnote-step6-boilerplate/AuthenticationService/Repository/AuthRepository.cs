using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Repository
{
    public class AuthRepository : IAuthRepository
    {
        //Define a private variable to represent AuthDbContext
        private readonly AuthDbContext db;
        public AuthRepository(AuthDbContext dbContext)
        {
            db = dbContext;
        }

        //This methos should be used to Create a new User
        public bool CreateUser(User user)
        {
            db.Users.Add(user);
            int result = db.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        //This methos should be used to check the existence of user
        public bool IsUserExists(string userId)
        {
            User user = db.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if(user == null)
            {
                return false;
            }
            return true;
        }

        //This methos should be used to Login a user
        public bool LoginUser(User user)
        {
            User u = db.Users.Where(x => x.UserId == user.UserId && x.Password == user.Password).FirstOrDefault();
            if(u == null)
            {
                return false;
            }
            return true;
        }
    }
}
