using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    //The class "User" will be acting as the data model for the User Table in the database. 
    public class User
    {
        /*
	     * This class should have five fields (userId,userName,
	     * password,contact,createdAt). Out of these five fields, the field
	     * userId should be the primary key.The value of createdAt should not be accepted from
	     * the user but should be always initialized with the system date
	     */
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
