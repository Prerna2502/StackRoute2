using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KeepNote.DAL.Entities;

namespace KeepNote.DAL
{
  public class UserRepository
  {

        /*
          Declare variables of type SqlConnection and SqlCommand
        */
        static SqlConnection con;
        static SqlDataAdapter adp;
        static DataSet ds;

    public UserRepository(string connectionString)
    {
            /*
              1. create SqlConnection instance with connectionString passed
              2. create SqlDataAdapter instance for users table
              3. create DataSet instance
              4. populate DataSet with records fetched

             */
            con = new SqlConnection(connectionString);
            adp = new SqlDataAdapter("select * from users", con);
            ds = new DataSet();
            adp.Fill(ds, "users");
    }

    public List<User> GetAllUsers()
    {
            /*
              1. Traverse through the rows in table Users of DataSet
              2. For each row, populate the user object
              3. Populate list with user object
              4. return the list
             */
            List<User> users = new List<User>();
            User u = new User();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                u.UserId = Convert.ToInt32(dr["userid"]);
                u.UserName = dr["username"].ToString();
                u.Password = dr["password"].ToString();
                u.Email = dr["email"].ToString();
                users.Add(u);
            }
            return users;
    }

    public int AddUser(User user)
    {

            /*
              1. create new DataRow
              2. populate the new DataRow with user values
              3. add this DataRow to the Rows of DataTable for Users 
              4. return the count of records
            */
            DataRow dr = ds.Tables[0].NewRow();
            dr["userId"] = user.UserId;
            dr["username"] = user.UserName;
            dr["password"] = user.Password;
            dr["email"] = user.Email;
            ds.Tables[0].Rows.Add(dr);
            return SaveChanges();

    }

    public int SaveChanges()
    {
            /*
              using SqlCommandBuilder update the Users table with User Records from DataSet

             */
            return adp.Update(ds);
    }
  }
}
