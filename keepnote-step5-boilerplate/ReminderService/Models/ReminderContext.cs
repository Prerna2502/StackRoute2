using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ReminderService.Models
{
    public class ReminderContext
    {
        //declare variables to connect to MongoDB database
        MongoClient client;
        IMongoDatabase db;

        public ReminderContext(IConfiguration configuration)
        {
            //Initialize MongoClient and Database using connection string and database name from configuration
            client = new MongoClient(configuration.GetConnectionString("MongoDBConnection"));
            db = client.GetDatabase(configuration.GetSection("MongoDatabase").Value);
        }

        //Define a MongoCollection to represent the Reminders collection of MongoDB
        public IMongoCollection<Reminder> Reminders => db.GetCollection<Reminder>("Reminders");
    }
}

