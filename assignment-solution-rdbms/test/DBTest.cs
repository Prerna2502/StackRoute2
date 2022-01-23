using Test.Infrasetup;
using Xunit;

namespace Test
{
    [TestCaseOrderer("Test.Infrasetup.PriorityOrderer", "test")]
    public class DBTest : IClassFixture<DataAccess>
    {
        DataAccess dal;
        public DBTest(DataAccess dataAccess)
        {
            dal = dataAccess;
        }

        #region table create test
        [Fact,TestPriority(1)]
        public void UserTableShouldBeCreated()
        {
            dal.CreateTable(dal.queries.SelectToken("UserTableScript").ToString());
            var actual = dal.IsTableCreated("user");
            Assert.True(actual);
        }

        [Fact, TestPriority(2)]
        public void NoteTableShouldBeCreated()
        {
            dal.CreateTable(dal.queries.SelectToken("NoteTableScript").ToString());
            var actual= dal.IsTableCreated("note");

            Assert.True(actual);
        }

        [Fact, TestPriority(3)]
        public void CategoryTableShouldBeCreated()
        {
            dal.CreateTable(dal.queries.SelectToken("CategoryTableScript").ToString());
            var actual = dal.IsTableCreated("category");
            Assert.True(actual);
        }

        [Fact, TestPriority(4)]
        public void ReminderTableShouldBeCreated()
        {
            dal.CreateTable(dal.queries.SelectToken("ReminderTableScript").ToString());
            var actual = dal.IsTableCreated("reminder");
            Assert.True(actual);
        }

        [Fact, TestPriority(5)]
        public void NoteCategoryTableShouldBeCreated()
        {
            dal.CreateTable(dal.queries.SelectToken("NoteCategoryTableScript").ToString());
            var actual = dal.IsTableCreated("notecategory");
            Assert.True(actual);
        }
        [Fact, TestPriority(6)]
        public void NoteReminderTableShouldBeCreated()
        {
            dal.CreateTable(dal.queries.SelectToken("NoteReminderTableScript").ToString());
            var actual = dal.IsTableCreated("notereminder");
            Assert.True(actual);
        }

        #endregion

        #region Insert data test

        [Fact, TestPriority(7)]
        public void UserDataShouldBeInserted()
        {
            string query = dal.queries.SelectToken("InsertScripts").SelectToken("User").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.True(actual > 0);
        }

        [Fact, TestPriority(8)]
        public void NoteDataShouldBeInserted()
        {
            string query = dal.queries.SelectToken("InsertScripts").SelectToken("Note").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.True(actual > 0);
        }

        [Fact, TestPriority(9)]
        public void CategoryDataShouldBeInserted()
        {
            string query = dal.queries.SelectToken("InsertScripts").SelectToken("Category").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.True(actual > 0);
        }

        [Fact, TestPriority(10)]
        public void ReminderDataShouldBeInserted()
        {
            string query = dal.queries.SelectToken("InsertScripts").SelectToken("Reminder").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.True(actual > 0);
        }

        [Fact, TestPriority(11)]
        public void NoteCategoryDataShouldBeInserted()
        {
            string query = dal.queries.SelectToken("InsertScripts").SelectToken("NoteCategory").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.True(actual > 0);
        }

        [Fact, TestPriority(12)]
        public void NoteReminderDataShouldBeInserted()
        {
            string query = dal.queries.SelectToken("InsertScripts").SelectToken("NoteReminder").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.True(actual > 0);
        }

        #endregion

        
        #region queries test

        //Q1 Fetch the row from User table based on Id and Password
        [Fact, TestPriority(13)]
        public void Query1ShouldReturnUserDetails()
        {
            var query = dal.queries.SelectToken("Query1").ToString();
            var actual = dal.GetTableData(query);

            Assert.True(actual.Count == 1);
            Assert.Equal("Maya", actual[0][1].ToString());
        }

        //Q2 Fetch all the rows from Note table based on the field note_creation_date.
        [Fact, TestPriority(14)]
        public void Query2ShouldReturnNoteDetails()
        {
            var query = dal.queries.SelectToken("Query2").ToString();
            var actual = dal.GetTableData(query);

            Assert.True(actual.Count == 1);
            Assert.Equal(2,(int)actual[0][0]);
            Assert.Equal("Training to plan", actual[0][1].ToString());
        }

        //Q3 Fetch all the Categories created after the particular Date
        [Fact, TestPriority(15)]
        public void Query3ShouldReturnCategoryDetails()
        {
            var query = dal.queries.SelectToken("Query3").ToString();
            var actual = dal.GetTableData(query);

            Assert.True(actual.Count == 1);
            Assert.Equal(2, (int)actual[0][0]);
            Assert.Equal("Diet", actual[0][1].ToString());
        }

        //Q4 Fetch all the Categories for note_id=1
        [Fact, TestPriority(16)]
        public void Query4ShouldReturnCategoryDetails()
        {
            var query = dal.queries.SelectToken("Query4").ToString();
            var actual = dal.GetTableData(query);

            Assert.True(actual.Count == 1);
            Assert.Equal(1, (int)actual[0][0]);
            Assert.Equal("Official", actual[0][1].ToString());
        }

        //Q5 Fetch all the Notes with user_id=112244
        [Fact, TestPriority(17)]
        public void Query5ShouldReturnNoteDetails()
        {
            var query = dal.queries.SelectToken("Query5").ToString();
            var actual = dal.GetTableData(query);

            Assert.True(actual.Count == 2);
            Assert.Equal(2, (int)actual[0][0]);
            Assert.Equal("Yet To Start", actual[0][3].ToString());
        }

        //Q6 Fetch all the Notes witg category_id=1
        [Fact, TestPriority(18)]
        public void Query6ShouldReturnNoteDetails()
        {
            var query = dal.queries.SelectToken("Query6").ToString();
            var actual = dal.GetTableData(query);

            Assert.True(actual.Count == 2);
            Assert.Equal(1, (int)actual[0][0]);
            Assert.Equal("Today Tasks", actual[0][1].ToString());
        }

        //Q7 Fetch all the Reminders for note_id=2
        [Fact, TestPriority(19)]
        public void Query7ShouldReturnReminderForANote()
        {
            var query = dal.queries.SelectToken("Query7").ToString();
            var actual = dal.GetTableData(query);

            Assert.True(actual.Count == 1);
            Assert.Equal(1, (int)actual[0][0]);
            Assert.Equal("KT reminder", actual[0][1].ToString());
        }

        //Q8 change the note_status to ‘Completed’ with note Id=3
        [Fact, TestPriority(20)]
        public void Query8ShouldUpdateNoteStatus()
        {
            var query = dal.queries.SelectToken("Query8").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.Equal(1,actual);

            var updated = dal.GetTableData("Select note_status from note where note_id=3");
            Assert.Equal("Completed",updated[0][0]);
        }

        //Q9 set ‘Personal Reminder’ for Note with note id=1
        [Fact, TestPriority(21)]
        public void Query9ShouldCreateReminderForANote()
        {
            var query = dal.queries.SelectToken("Query9").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.Equal(1, actual);

            var notereminder = dal.GetTableData("Select * from notereminder where note_id=1");
            Assert.Equal(3,(int)notereminder[0][0]);
        }

        //Q10 remove all reminders from Note with note_id=2
        [Fact, TestPriority(23)]
        public void Query11ShouldDeleteRemindersForANote()
        {
            var query = dal.queries.SelectToken("Query10").ToString();
            var actual = dal.ExecuteCUDQuery(query);

            Assert.Equal(1, actual);
        }
        
        #endregion

    }
}
