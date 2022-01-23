using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace Entities
{
    // The class "Reminder" will be acting as the data model for the Reminder Table in the database. 
    public class Reminder
    {
		/*
	     * This class should have eight fields
	     * (reminderId,reminderName,reminderDescription,reminderType,
	     * reminderCreatedBy,reminderCreationDate,notes, user). Out of these eight fields, the
	     * field reminderId should be primary key and auto-generated. The value of
	     * reminderCreationDate should not be accepted from the user but should be
	     * always initialized with the system date. annotate notes and user field with [JsonIgnore]
	     */
		[Key]
		public int ReminderId { get; set; }
		public string ReminderName { get; set; }
		public string ReminderDescription { get; set; }
		public string ReminderType { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		[JsonIgnore]
		public virtual ICollection<Note> Notes { get; set; }

		[JsonIgnore]
		public virtual User User { get; set; }
	}
}
