using System;
using System.Collections.Generic;
using System.Linq;
using ReminderService.Exceptions;
using ReminderService.Models;
using ReminderService.Repository;

namespace ReminderService.Service
{
    public class ReminderService : IReminderService
    {
        //define a private variable to represent repository
        private readonly IReminderRepository _repository;

        //Use constructor Injection to inject all required dependencies.

        public ReminderService(IReminderRepository reminderRepository)
        {
            _repository = reminderRepository;
        }

        //This method should be used to save a new reminder.
        public Reminder CreateReminder(Reminder reminder)
        {
            Reminder reminder1 = _repository.GetReminderById(reminder.Id);
            if (reminder1 == null)
            {
                return _repository.CreateReminder(reminder);
            }
            throw new ReminderNotCreatedException("This reminder already exists");
        }
        //This method should be used to delete an existing reminder.
        public bool DeleteReminder(int reminderId)
        {
            if (_repository.DeleteReminder(reminderId))
            {
                return true;
            }
            throw new ReminderNotFoundException("This reminder id not found");
        }
        // This method should be used to get all reminders by userId
        public List<Reminder> GetAllRemindersByUserId(string userId)
        {
            return _repository.GetAllRemindersByUserId(userId);
        }
        //This method should be used to get a reminder by reminderId.
        public Reminder GetReminderById(int reminderId)
        {
            Reminder reminder = _repository.GetReminderById(reminderId);
            if (reminder != null)
            {
                return reminder;
            }
            throw new ReminderNotFoundException("This reminder id not found");
        }
        //This method should be used to update an existing reminder.
        public bool UpdateReminder(int reminderId, Reminder reminder)
        {
            Reminder reminder1 = _repository.GetReminderById(reminderId);
            if (reminder1 != null)
            {
                return _repository.UpdateReminder(reminderId, reminder);
            }
            throw new ReminderNotFoundException("This reminder id not found");
        }
    }
}
