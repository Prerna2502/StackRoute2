using System;
using System.Collections.Generic;
using DAL;
using Entities;
using Exceptions;

namespace Service
{
    /*
   * Service classes are used here to implement additional business logic/validation
   * */
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository repository;
        /*
        Use constructor Injection to inject all required dependencies.
        */
        public ReminderService(IReminderRepository reminderRepository)
        {
            repository = reminderRepository;
        }

        //This method should be used to save a new reminder.
        public Reminder CreateReminder(Reminder reminder)
        {
            Reminder r = repository.GetReminderById(reminder.ReminderId);
            if (r == null)
            {
                return repository.CreateReminder(reminder);
            }
            throw new Exception($"Reminder with id: {reminder.ReminderId} does not exist");
        }

        //This method should be used to delete an existing reminder.
        public bool DeletReminder(int reminderId)
        {
            bool result = repository.DeletReminder(reminderId);
            if(result)
            {
                return result;
            }
            throw new ReminderNotFoundException($"Reminder with id: {reminderId} does not exist");
        }

        //This method should be used to get all reminder by userId.
        public List<Reminder> GetAllRemindersByUserId(string userId)
        {
            return repository.GetAllRemindersByUserId(userId);
        }
        //This method should be used to get a reminder by reminderId.
        public Reminder GetReminderById(int reminderId)
        {
            Reminder r = repository.GetReminderById(reminderId);
            if (r != null)
            {
                return r;
            }
            throw new ReminderNotFoundException($"Reminder with id: {reminderId} does not exist");
        }

        // This method should be used to update a existing reminder.
        public bool UpdateReminder(int reminderId,Reminder reminder)
        {
            Reminder r = repository.GetReminderById(reminderId);
            if (r != null)
            {
                return repository.UpdateReminder(reminder);
            }
            throw new ReminderNotFoundException($"Reminder with id: {reminderId} does not exist");
        }
    }
}
