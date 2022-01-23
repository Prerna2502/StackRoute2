using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CategoryService.Models
{
    public class CategoryContext
    {
        //declare variable to connect to MongoDB database
        MongoClient client;
        IMongoDatabase db;

        public CategoryContext(IConfiguration configuration)
        {
            //Initialize MongoClient and Database using connection string and database name from configuration
            client = new MongoClient(configuration.GetConnectionString("MongoDBConnection"));
            db = client.GetDatabase(configuration.GetSection("MongoDatabase").Value);
        }

        //Define a MongoCollection to represent the Categories collection of MongoDB
        public IMongoCollection<Category> Categories => db.GetCollection<Category>("Categories");
    }
}