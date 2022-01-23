using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using ReminderService.Models;

namespace ReminderService.Repository
{
    public class ReminderRepository:IReminderRepository
    {
        //define a private variable to represent ReminderContext
        private readonly ReminderContext context;
        public ReminderRepository(ReminderContext _context)
        {
            context = _context;
        }
        //This method should be used to save a new reminder.
        public Reminder CreateReminder(Reminder reminder)
        {
            //reminder Id should be auto generated and must start from 201
            var sd = context.Reminders.Find(x => true).SortByDescending(d => d.Id).Limit(1).FirstOrDefaultAsync();
            reminder.Id = sd.Result.Id + 1;
            context.Reminders.InsertOne(reminder);
            return context.Reminders.Find(x => x.Id == reminder.Id).FirstOrDefault();
        }
        //This method should be used to delete an existing reminder.
        public bool DeleteReminder(int reminderId)
        {
            context.Reminders.DeleteOne(x => x.Id == reminderId);
            return true;
        }
        //This method should be used to get all reminders by userId
        public List<Reminder> GetAllRemindersByUserId(string userId)
        {
            return context.Reminders.Find(x => x.CreatedBy == userId).ToList();
        }
        //This method should be used to get a reminder by reminderId
        public Reminder GetReminderById(int reminderId)
        {
            return context.Reminders.Find(x => x.Id == reminderId).FirstOrDefault();
        }
        // This method should be used to update an existing reminder.
        public bool UpdateReminder(int reminderId, Reminder reminder)
        {
            var filter = Builders<Reminder>.Filter.Where(x => x.Id == reminderId);
            var update = Builders<Reminder>.Update.Set(x => x.Name, reminder.Name)
                .Set(x => x.Description, reminder.Description)
                .Set(x => x.CreationDate, reminder.CreationDate)
                .Set(x => x.CreatedBy, reminder.CreatedBy)
                .Set(x => x.Type, reminder.Type);
            context.Reminders.UpdateOne(filter, update);
            return true;
        }
    }
}
