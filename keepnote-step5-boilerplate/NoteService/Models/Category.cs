using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NoteService.Models
{
    public class Category
    {
        /*
        This class should have five properties
        (Id,Name,Description,CreatedBy,CreationDate).The value of CreationDate should not
        be accepted from the user but should be always initialized with the system
        date. 
        */
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
    }
}
