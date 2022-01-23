using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace UserService.Models
{
    public class UserContext
    {
        MongoClient client;
        IMongoDatabase db;
        public UserContext(IConfiguration configuration)
        {
            //Initialize MongoClient and Database using connection string and database name from configuration
            client = new MongoClient(configuration.GetConnectionString("MongoDBConnection"));
            db = client.GetDatabase(configuration.GetSection("MongoDatabase").Value);
        }

        //Define a MongoCollection to represent the Users collection of MongoDB
        public IMongoCollection<User> Users => db.GetCollection<User>("Users");
    }
}
