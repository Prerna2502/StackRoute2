using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteService.Models
{
    public class NoteContext
    {
        //declare variables to connect to MongoDB database
        MongoClient client;
        IMongoDatabase db;

        public NoteContext(IConfiguration configuration)
        {
            //Initialize MongoClient and Database using connection string and database name from configuration
            client = new MongoClient(configuration.GetConnectionString("MongoDBConnection"));
            db = client.GetDatabase(configuration.GetSection("MongoDatabase").Value);
        }

        //Define a MongoCollection to represent the Notes collection of MongoDB based on NoteUser type
        public IMongoCollection<NoteUser> Notes => db.GetCollection<NoteUser>("Notes");
    }
}
