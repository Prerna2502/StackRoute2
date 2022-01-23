using KeepNote.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace KeepNote.DAL
{
  public class NoteRepository
  {
        /*
          Declare variables of type SqlConnection and SqlCommand
        */
        static SqlConnection con;
        static SqlCommand cmd1;
        static SqlCommand cmd2;
    public NoteRepository(string connectionString)
    {
            /*
              Instantiate SqlConnection object with the connectionString passed to the constructor
              Instantiate SqlCommand object
             */
            con = new SqlConnection(connectionString);
            cmd1 = new SqlCommand();
            cmd2 = new SqlCommand();
    }

    //Read all notes 
    public List<Note> GetAllNotes()
    {
            /*
              1. open connection
              2. set the command text of SqlCommand object with appropriate query to read all notes
              3. using ExecuteReader() method of SqlCommand object fetch data
              4. Recursively read the records fetced one by one and populate the note object
              5. Populate the list object with note object on each iteration
              6. close the connection
              7. Return the populated list
            */
            con.Open();
            cmd1.CommandText = "Select * from notes";
            cmd1.Connection = con;
            SqlDataReader dr = cmd1.ExecuteReader();
            List<Note> notes = new List<Note>();
            Note n = new Note();
            while (dr.Read())
            {
                n.NoteId = Convert.ToInt16(dr["noteid"]);
                n.Title = dr["title"].ToString();
                n.Description = dr["description"].ToString();
                n.CreatedBy = Convert.ToInt16(dr["createdby"]);
                notes.Add(n);
            }
            con.Close();
            return notes;
    }

    public int AddNote(Note note)
    {

            /*
              1. open connection
              2. set the command text of SqlCommand object with appropriate query to insert note record
              3. execute ExecuteNonQuery() method 
              4. close the connection
              5. return the count of records
            */
            con.Open();
            cmd2.CommandText = "Insert into notes values(@id,@title,@description,@createdBy)";
            cmd2.Connection = con;
            cmd2.Parameters.AddWithValue("@id", note.NoteId);
            cmd2.Parameters.AddWithValue("@title", note.Title);
            cmd2.Parameters.AddWithValue("@description", note.Description);
            cmd2.Parameters.AddWithValue("@createdBy", note.CreatedBy);
            int count = cmd2.ExecuteNonQuery();
            con.Close();
            return count;
        }
  }
}
