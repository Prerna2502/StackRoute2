using System;
using System.Collections.Generic;
using DAL;
using Entities;
using Exceptions;

namespace Service
{
    /*
   * Service classes are used here to implement additional business logic/validation
   * */
    public class NoteService : INoteService
    {
        private readonly INoteRepository _repository;
        private readonly ICategoryRepository _category;
        private readonly IReminderRepository _reminder;
        /*
         Use constructor Injection to inject all required dependencies.
             */
        public NoteService(INoteRepository repository, ICategoryRepository category, IReminderRepository reminder)
        {
            _repository = repository;
            _category = category;
            _reminder = reminder;
        }

        /*
	     * This method should be used to save a new note.
	     */
        public Note CreateNote(Note note)
        {
            Category category = _category.GetCategoryById(note.CategoryId);
            if(category == null)
            {
                throw new CategoryNotFoundException($"Category with id: {note.CategoryId} does not exist");
            }
            Reminder reminder = _reminder.GetReminderById(note.ReminderId);
            if(reminder == null)
            {
                throw new ReminderNotFoundException($"Reminder with id: {note.ReminderId} does not exist");
            }
            Note n = _repository.GetNoteByNoteId(note.NoteId);
            if (n == null)
            {
                return _repository.CreateNote(note);
            }
            throw new Exception($"Note with id: {note.NoteId} already exist");
        }
        /* This method should be used to delete an existing note. */
        public bool DeleteNote(int noteId)
        {
            bool result = _repository.DeleteNote(noteId);
            if (result)
            {
                return result;
            }
            throw new NoteNotFoundException($"Note with id: {noteId} does not exist");
        }

        /*
	     * This method should be used to get all note by userId.
	     */
        public List<Note> GetAllNotesByUserId(string userId)
        {
            return _repository.GetAllNotesByUserId(userId);
        }

        //This method should be used to get a note by noteId.
        public Note GetNoteByNoteId(int noteId)
        {
            Note n = _repository.GetNoteByNoteId(noteId);
            if (n != null)
            {
                return n;
            }
            throw new NoteNotFoundException($"Note with id: {noteId} does not exist");
        }
        //This method should be used to update a existing note.
        public bool UpdateNote(int noteId,Note note)
        {
            Note n = _repository.GetNoteByNoteId(noteId);
            if (n != null)
            {
                Category category = _category.GetCategoryById(note.CategoryId);
                if (category == null)
                {
                    throw new CategoryNotFoundException($"Category with id: {note.CategoryId} does not exist");
                }
                Reminder reminder = _reminder.GetReminderById(note.ReminderId);
                if (reminder == null)
                {
                    throw new ReminderNotFoundException($"Reminder with id: {note.ReminderId} does not exist");
                }
                note.NoteId = noteId;
                return _repository.UpdateNote(note);
            }
            throw new NoteNotFoundException($"Note with id: {noteId} does not exist");
        }
    }
}
