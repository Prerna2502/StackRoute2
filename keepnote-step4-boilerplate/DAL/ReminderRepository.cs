using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace DAL
{
    //Repository class is used to implement all Data access operations
    public class ReminderRepository : IReminderRepository
    {
        private readonly KeepDbContext db;
        public ReminderRepository(KeepDbContext dbContext)
        {
            db = dbContext;
        }
        //This method should be used to save a new reminder.
        public Reminder CreateReminder(Reminder reminder)
        {
            db.Reminders.Add(reminder);
            db.SaveChanges();
            return db.Reminders.Where(r => r.ReminderId == reminder.ReminderId).FirstOrDefault();
        }
        //This method should be used to delete an existing reminder.
        public bool DeletReminder(int reminderId)
        {
            Reminder reminder = db.Reminders.Where(r => r.ReminderId == reminderId).FirstOrDefault();
            if(reminder == null)
            {
                return false;
            }
            db.Reminders.Remove(reminder);
            db.SaveChanges();
            Reminder reminderFound = db.Reminders.Where(r => r.ReminderId == reminderId).FirstOrDefault();
            if (reminderFound == null)
            {
                return true;
            }
            return false;
        }
        //This method should be used to get all reminder by userId.
        public List<Reminder> GetAllRemindersByUserId(string userId)
        {
            return db.Reminders.Where(r => r.CreatedBy == userId).ToList();
        }
        //This method should be used to get a reminder by reminderId.
        public Reminder GetReminderById(int reminderId)
        {
            return db.Reminders.Where(r => r.ReminderId == reminderId).FirstOrDefault();
        }
        // This method should be used to update a existing reminder.
        public bool UpdateReminder(Reminder reminder)
        {
            var rem = db.Reminders.Where(r => r.ReminderId == reminder.ReminderId).FirstOrDefault();
            rem.ReminderName = reminder.ReminderName;
            rem.ReminderDescription = reminder.ReminderDescription;
            rem.ReminderType = reminder.ReminderType;
            rem.CreatedBy = reminder.CreatedBy;
            db.Entry<Reminder>(rem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return true;
        }
    }
}
