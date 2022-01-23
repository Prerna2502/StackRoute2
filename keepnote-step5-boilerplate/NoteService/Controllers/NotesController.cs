using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NoteService.Exceptions;
using NoteService.Models;
using NoteService.Service;

namespace NoteService.Controllers
{
    /*
      As in this assignment, we are working with creating RESTful web service to create microservices, hence annotate
      the class with [ApiController] annotation and define the controller level route as per REST Api standard.
  */
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : Controller
    {
        /*
        NoteService should  be injected through constructor injection. Please note that we should not create service
        object using the new keyword
        */
        private readonly INoteService service;
        public NotesController(INoteService _service)
        {
            service = _service;
        }

        /*
	    * Define a handler method which will create a specific note by reading the
	    * Serialized object from request body and save the note details in the
	    * database.This handler method should return any one of the status messages
	    * basis on different situations: 
	    * 1. 201(CREATED) - If the note created successfully. 
	    
	    * This handler method should map to the URL "/api/note/{userId}" using HTTP POST method
	    */
        [HttpPost("{userId}")]
        public IActionResult Post(string userId, [FromBody]Note note)
        {
            try
            {
                bool result = service.CreateNote(note);

                return StatusCode(201, note);
            }
            catch (Exception e)
            {
                return StatusCode(409, e.Message);
            }
        }

        /*
         * Define a handler method which will delete a note from a database.
         * This handler method should return any one of the status messages basis 
         * on different situations: 
         * 1. 200(OK) - If the note deleted successfully from database. 
         * 2. 404(NOT FOUND) - If the note with specified noteId is not found.
         *
         * This handler method should map to the URL "/api/note/{userId}/{noteId}" using HTTP Delete
         */
        [HttpDelete("{userId}/{noteId}")]
        public IActionResult Delete(int noteId, string userId)
        {
            try
            {
                Note note = service.GetAllNotesByUserId(userId).Find(x => x.Id == noteId);
                if(note == null)
                {
                    throw new NoteNotFoundExeption($"NoteId {noteId} for user {userId} does not exist");
                }
                bool result = service.DeleteNote(userId, noteId);
                return StatusCode(200, result);
            }
            catch (NoteNotFoundExeption e)
            {
                return StatusCode(404, e.Message);
            }
        }

        /*
         * Define a handler method which will update a specific note by reading the
         * Serialized object from request body and save the updated note details in a
         * database. 
         * This handler method should return any one of the status messages
         * basis on different situations: 
         * 1. 200(OK) - If the note updated successfully.
         * 2. 404(NOT FOUND) - If the note with specified noteId is not found.
         * 
         * This handler method should map to the URL "/api/note/{userId}/{noteId}" using HTTP PUT method.
         */
        [HttpPut("{userId}/{noteId}")]
        public IActionResult Put(string userId, int noteId, Note note)
        {
            try
            {
                Note s = service.UpdateNote(noteId, userId, note);
                return StatusCode(200, s);
            }
            catch (NoteNotFoundExeption e)
            {
                return StatusCode(404, e.Message);
            }
        }

        /*
         * Define a handler method which will get us the all notes by a userId.
         * This handler method should return any one of the status messages basis on
         * different situations: 
         * 1. 200(OK) - If the note found successfully. 
         * 
         * This handler method should map to the URL "/api/note/{userId}" using HTTP GET method
         */
        [HttpGet("{userId}")]
        public IActionResult Get(string userId)
        {
            try
            {
                return StatusCode(200, service.GetAllNotesByUserId(userId));
            }
            catch (NoteNotFoundExeption e)
            {
                return StatusCode(404, e.Message);
            }
        }

        /*
         * Define a handler method which will show details of a specific note created by specific 
         * user. This handler method should return any one of the status messages basis on
         * different situations: 
         * 1. 200(OK) - If the note found successfully. 
         * 2. 404(NOT FOUND) - If the note with specified noteId is not found.
         * This handler method should map to the URL "/api/note/{userId}/{noteId}" using HTTP GET method
         * where "id" should be replaced by a valid reminderId without {}
         * 
         */
        [HttpGet("{userId}/{noteId}")]
        public IActionResult Get(string userId, int noteId)
        {
            try
            {
                List<Note> notes = service.GetAllNotesByUserId(userId);
                Note note = notes.Find(x => x.Id == noteId);
                if(note == null)
                {
                    throw new NoteNotFoundExeption("Note not found");
                }
                return StatusCode(200, note);
            }
            catch (NoteNotFoundExeption e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}
