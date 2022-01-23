using System.Collections.Generic;
using System.Linq;
using Entities;

namespace DAL
{
    //Repository class is used to implement all Data access operations
    public class NoteRepository : INoteRepository
    {
        private readonly KeepDbContext db;
        public NoteRepository(KeepDbContext dbContext)
        {
            db = dbContext;
        }

        // This method should be used to save a new note.
        public Note CreateNote(Note note)
        {
            db.Notes.Add(note);
            db.SaveChanges();
            return db.Notes.Where(n => n.NoteId == note.NoteId).FirstOrDefault();
        }

        /* This method should be used to delete an existing note. */
        public bool DeleteNote(int noteId)
        {
            var note = db.Notes.Where(n => n.NoteId == noteId).FirstOrDefault();
            db.Notes.Remove(note);
            db.SaveChanges();
            var noteFound = db.Notes.Where(n => n.NoteId == noteId).FirstOrDefault();
            if (noteFound == null)
            {
                return true;
            }
            return false;
        }

        //* This method should be used to get all note by userId.
        public List<Note> GetAllNotesByUserId(string userId)
        {
            return db.Notes.Where(n => n.CreatedBy == userId).ToList();
        }
        //This method should be used to get a note by noteId.
        public Note GetNoteByNoteId(int noteId)
        {
            return db.Notes.Where(n => n.NoteId == noteId).FirstOrDefault();
        }
        //This method should be used to update a existing note.
        public bool UpdateNote(Note note)
        {
            var noteToUpdate = db.Notes.Where(n => n.NoteId == note.NoteId).FirstOrDefault();
            noteToUpdate.NoteContent = note.NoteContent;
            noteToUpdate.NoteStatus = note.NoteStatus;
            noteToUpdate.NoteTitle = note.NoteTitle;
            noteToUpdate.ReminderId = note.ReminderId;
            noteToUpdate.CategoryId = note.CategoryId;
            noteToUpdate.CreatedBy = note.CreatedBy;
            db.Entry<Note>(noteToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return true;
        }
    }
}
