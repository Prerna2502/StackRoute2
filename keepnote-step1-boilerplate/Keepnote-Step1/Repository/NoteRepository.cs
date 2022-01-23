using System.Collections.Generic;
using Keepnote_Step1.Models;

namespace Keepnote_Step1.Repository
{
      /*
      This class contains the code for data storage interactions and methods 
      of this class will be used by other parts of the applications such
      as Controllers and Test Cases
      */
    public class NoteRepository : INoteRepository
    {
        /* Declare a variable of List type to store all the notes. */
        private static List<Note> Notes;

        public NoteRepository()
        {
            /* Initialize the variable using proper data type */
            Notes = new List<Note>();
        }

        /* This method should return all the notes in the list */
        public List<Note> GetNotes()
        {
            return Notes;
        }

        /* This method should return the specific note in the list */
        public Note GetNote(int noteId)
        {
            if (Exists(noteId))
            {
                return Notes.Find(n => n.NoteId == noteId);
            }
            return null;
        }

        /*
	        This method should accept Note object as argument and add the new note object into  list
	    */
        public void AddNote(Note note)
        {
            Notes.Add(note);
        }

        /* This method should deleted a specified note from the list */
        public bool DeletNote(int noteId)
        {
            if(Exists(noteId))
            {
                Note note = Notes.Find(n => n.NoteId == noteId);
                Notes.Remove(note);
                return true;
            }
            return false;
        }

        /*
	      This method should check if the matching note id present in the list or not.
	      Return true if note id exists in the list or return false if note id does not
	      exists in the list
	  */
        public bool Exists(int noteId)
        {
            if(Notes.Exists(n => n.NoteId == noteId))
            {
                return true;
            }
            return false;
        }
    }
}
