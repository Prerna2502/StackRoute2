using System;
using System.Collections.Generic;
using NoteService.Exceptions;
using NoteService.Models;
using NoteService.Repository;

namespace NoteService.Service
{
    public class NoteService : INoteService
    {
        //define a private variable to represent repository
        private readonly INoteRepository _repository;

        //Use constructor Injection to inject all required dependencies.

        public NoteService(INoteRepository _noteRepository)
        {
            _repository = _noteRepository;
        }
        
        //This method should be used to create a new note.
        public bool CreateNote(Note note)
        {
            if (_repository.CreateNote(note))
            {
                return true;
            }
            throw new NoteAlreadyExistsException("This Note already exists");
        }

        //This method should be used to delete an existing note for a user
        public bool DeleteNote(string userId, int noteId)
        {
            if (_repository.DeleteNote(userId, noteId))
            {
                return true;
            }
            throw new NoteNotFoundExeption($"NoteId {noteId} for user {userId} does not exist");
        }

        //This methos is used to retreive all notes for a user
        public List<Note> GetAllNotesByUserId(string userId)
        {
            return _repository.FindAllNotesByUser(userId);
        }

        //This method is used to update an existing note for a user
        public Note UpdateNote(int noteId, string userId, Note note)
        {
            if (_repository.UpdateNote(noteId, userId, note))
            {
                return note;
            }
            throw new NoteNotFoundExeption($"NoteId {noteId} for user {userId} does not exist");
        }
        
    }
}
