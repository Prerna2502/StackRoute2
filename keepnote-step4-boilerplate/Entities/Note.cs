using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    //The class "Note" will be acting as the data model for the Note Table in the database. 
    public class Note
    {
		/*
	 * This class should have eight properties
	 * (noteId,noteTitle,noteContent,noteStatus,createdAt,
	 * categoryId,reminderId, and createdBy). Out of these eight fields, the field noteId
	 * should be primary key and auto-generated. The value of createdAt should not be
	 * accepted from the user but should be always initialized with the system date.
	 * Another 3 navigation properties-> category, reminder and user should use [JsonIgnore].
	 */
		[Key]
		public int NoteId { get; set; }
		public string NoteTitle { get; set; }
		public string NoteContent { get; set; }
		public string NoteStatus { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public int CategoryId { get; set; }
		public int ReminderId { get; set; }
		public string CreatedBy { get; set; }

		[JsonIgnore]
		public virtual Category Category { get; set; }

		[JsonIgnore]
		public virtual Reminder Reminder { get; set; }

		[JsonIgnore]
		public virtual User User { get; set; }
	}
}
