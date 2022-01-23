using System.Collections.Generic;
using System.Linq;
using Keepnote.Models;
using Microsoft.EntityFrameworkCore;

namespace Keepnote.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly KeepNoteContext db;
        public NoteRepository(KeepNoteContext _db)
        {
            db = _db;
        }

        // Save the note in the database(note) table.
        public int AddNote(Note note)
        {
            note.CreatedAt = System.DateTime.Now;
            db.Notes.Add(note);
            return db.SaveChanges();
        }
        //Remove the note from the database(note) table.
        public int DeletNote(int noteId)
        {
            Note note = db.Notes.Where(n => n.NoteId == noteId).FirstOrDefault();
            db.Notes.Remove(note);
            return db.SaveChanges();
        }
        
        //can be used as helper method for controller
        public bool Exists(int noteId)
        {
            Note note = db.Notes.Where(n => n.NoteId == noteId).FirstOrDefault();
            if(note == null)
            {
                return false;
            }
            return true;
        }

       /* retrieve all existing notes sorted by created Date in descending
        order(showing latest note first)*/
        public List<Note> GetAllNotes()
        {
            return db.Notes.ToList();
        }

        //retrieve specific note from the database(note) table
        public Note GetNoteById(int noteId)
        {
            return db.Notes.Where(n => n.NoteId == noteId).FirstOrDefault();
        }
        //Update existing note
        public int UpdateNote(Note note)
        {
            Note note1 = db.Notes.Where(n => n.NoteId == note.NoteId).FirstOrDefault();
            note.NoteTitle = note1.NoteTitle;
            note.NoteContent = note1.NoteContent;
            note.CreatedAt = note1.CreatedAt;
            note.NoteStatus = note1.NoteStatus;
            db.Entry<Note>(note1).State = EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
