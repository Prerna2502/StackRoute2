using System.Collections.Generic;
using Keepnote.Models;

namespace Keepnote.Repository
{
    /* You Should not modify this interface.  You have to implement these methods in corresponding Impl class*/
    public interface INoteRepository
    {
        int AddNote(Note note);
        int DeletNote(int noteId);
        int UpdateNote(Note note);
        bool Exists(int noteId);
        List<Note> GetAllNotes();
        Note GetNoteById(int noteId);
    }
}