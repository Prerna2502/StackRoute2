using System.Collections.Generic;
using Entities;

namespace Service
{
    /*
    * Should not modify this interface. You have to implement these methods in
    * corresponding Impl classes
    */
    public interface INoteService
    {
        Note CreateNote(Note note);
        bool UpdateNote(int noteId,Note note);
        bool DeleteNote(int noteId);
        Note GetNoteByNoteId(int noteId);
        List<Note> GetAllNotesByUserId(string userId);
    }
}
