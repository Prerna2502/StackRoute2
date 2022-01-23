using System.Collections.Generic;
using Entities;

namespace Service
{
    /*
    * Should not modify this interface. You have to implement these methods in
    * corresponding Impl classes
    */
    public interface IReminderService
    {
        Reminder CreateReminder(Reminder reminder);
        bool UpdateReminder(int reminderId,Reminder reminder);
        bool DeletReminder(int reminderId);
        Reminder GetReminderById(int reminderId);
        List<Reminder> GetAllRemindersByUserId(string userId);
    }
}
