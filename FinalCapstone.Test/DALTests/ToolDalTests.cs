using FinalCapstone.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Linq;
using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace FinalCapstone.Test.DALTests
{
    [TestClass]
    public class ToolDalTests : DatabaseTest
    {
        private IToolDal _toolDal;

        [TestInitialize]
        public void Setup()
        {
            _toolDal = new ToolDal(toolDBConnectionString);
        }

        [TestClass]
        public class GetAllTools : ToolDalTests
        {
            [TestMethod]
            public void NoToolsExist()
            {
                var tools = _toolDal.GetTools(false);
                Assert.IsFalse(tools.Any());
            }

            [TestMethod]
            public void GetToolsCheckedIn()
            {
                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    const string sql1 = "INSERT INTO member (member_name, drivers_license, email, member_address) VALUES ('Han Solo', 'BO085123', 'han@starmail.com', '123 Corellian St'); SELECT CAST(SCOPE_IDENTITY() as int);";

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = sql1;

                    conn.Open();
                    int memberId = (int)cmd.ExecuteScalar();

                    const string sql2 =
                       @"INSERT INTO tool (brand, tool_name, description, checked_out) VALUES ('fakeBrand1', 'TestTool1', 'Fake Description1', 0);
                         INSERT INTO tool (brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('fakeBrand2', 'TestTool2', 'Fake Description2', 1, @current_borrower, '2018-12-11', '2018-12-18');
                         INSERT INTO tool (brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('fakeBrand3', 'TestTool3', 'Fake Description3', 1, @current_borrower, '2018-12-11', '2018-12-18');";

                    cmd = conn.CreateCommand();
                    cmd.CommandText = sql2;
                    cmd.Parameters.AddWithValue("@current_borrower", memberId);

                    cmd.ExecuteNonQuery();
                }
                var checkedOutTools = _toolDal.GetTools(false);
                Assert.AreEqual(1, checkedOutTools.Count);
            }

            [TestMethod]
            public void GetToolsCheckedOut()
            {
                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    const string sql1 = "INSERT INTO member (member_name, drivers_license, email, member_address) VALUES ('Han Solo', 'BO085123', 'han@starmail.com', '123 Corellian St'); SELECT CAST(SCOPE_IDENTITY() as int);";

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = sql1;

                    conn.Open();
                    int memberId = (int)cmd.ExecuteScalar();

                    const string sql2 =
                       @"INSERT INTO tool (brand, tool_name, description, checked_out) VALUES ('fakeBrand1', 'TestTool1', 'Fake Description1', 0);
                         INSERT INTO tool (brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('fakeBrand2', 'TestTool2', 'Fake Description2', 1, @current_borrower, '2018-12-11', '2018-12-18');
                         INSERT INTO tool (brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('fakeBrand3', 'TestTool3', 'Fake Description3', 1, @current_borrower, '2018-12-11', '2018-12-18');";

                    cmd = conn.CreateCommand();
                    cmd.CommandText = sql2;
                    cmd.Parameters.AddWithValue("@current_borrower", memberId);

                    cmd.ExecuteNonQuery();
                }
                var checkedOutTools = _toolDal.GetTools(true);
                Assert.AreEqual(2, checkedOutTools.Count);
            }

            [TestMethod]
            public void CheckedInTest()
            {
                int toolId = 0;
                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    const string sql1 = "INSERT INTO member (member_name, drivers_license, email, member_address) VALUES ('Han Solo', 'BO085123', 'han@starmail.com', '123 Corellian St'); SELECT CAST(SCOPE_IDENTITY() as int); SELECT CAST(SCOPE_IDENTITY() as int);";

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = sql1;

                    conn.Open();
                    int memberId = (int)cmd.ExecuteScalar();

                    const string sql2 =
                       @"INSERT INTO tool (brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('fakeBrand2', 'TestTool2', 'Fake Description2', 0, @current_borrower, '2018-12-11', '2018-12-18'); SELECT CAST(SCOPE_IDENTITY() as int);";

                    cmd = conn.CreateCommand();
                    cmd.CommandText = sql2;
                    cmd.Parameters.AddWithValue("@current_borrower", memberId);

                    toolId = (int)cmd.ExecuteScalar();
                }
                Assert.IsFalse(_toolDal.CheckToolAvailability(toolId));
            }

            [TestMethod]
            public void CheckedOutTest()
            {
                int toolId = 0;
                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    const string sql1 = "INSERT INTO member (member_name, drivers_license, email, member_address) VALUES ('Han Solo', 'BO085123', 'han@starmail.com', '123 Corellian St'); SELECT CAST(SCOPE_IDENTITY() as int); SELECT CAST(SCOPE_IDENTITY() as int);";

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = sql1;

                    conn.Open();
                    int memberId = (int)cmd.ExecuteScalar();

                    const string sql2 =
                       @"INSERT INTO tool (brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('fakeBrand2', 'TestTool2', 'Fake Description2', 1, @current_borrower, '2018-12-11', '2018-12-18'); SELECT CAST(SCOPE_IDENTITY() as int);";

                    cmd = conn.CreateCommand();
                    cmd.CommandText = sql2;
                    cmd.Parameters.AddWithValue("@current_borrower", memberId);

                    toolId = (int)cmd.ExecuteScalar();
                }
                Assert.IsTrue(_toolDal.CheckToolAvailability(toolId));
            }

            [TestMethod]
            public void GetDetailsTest()
            {
                int toolId = 0;
                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    const string sql1 = "INSERT INTO member (member_name, drivers_license, email, member_address) VALUES ('Han Solo', 'BO085123', 'han@starmail.com', '123 Corellian St'); SELECT CAST(SCOPE_IDENTITY() as int);";

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = sql1;

                    conn.Open();
                    int memberId = (int)cmd.ExecuteScalar();

                    const string sql2 =
                       @"INSERT INTO tool (brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date) VALUES ('fakeBrand2', 'TestTool2', 'Fake Description2', 0, @current_borrower, '2018-12-11', '2018-12-18'); SELECT CAST(SCOPE_IDENTITY() as int);";

                    cmd = conn.CreateCommand();
                    cmd.CommandText = sql2;
                    cmd.Parameters.AddWithValue("@current_borrower", memberId);

                    toolId = (int)cmd.ExecuteScalar();
                }
                Tool tool = new Tool();
                tool = _toolDal.GetToolDetails(toolId, false);
                Assert.AreEqual("Fake Description2", tool.Description);
            }

            [TestMethod]
            public void GetUsersTest()
            {
                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO member (member_name, drivers_license, email, member_address) VALUES ('Fake Member_1', 'ABC1234', 'fake_member_1@techelevator.com', '1275 Kinnear Road');" +
                        "INSERT INTO member (member_name, drivers_license, email, member_address) VALUES ('Fake Member_2', 'DEF5678', 'fake_member_2@techelevator.com', '1275 Kinnear Road');", conn);

                    cmd.ExecuteNonQuery();
                }
                IList<User> users = _toolDal.GetUsers();
                Assert.AreEqual(2, users.Count);
                Assert.AreEqual("Fake Member_1", users[0].Name);
            }

            [TestMethod]
            public void ChangeCheckedOutStatusTest()
            {
                int memberId;
                int tool1Id;
                int tool2Id;

                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO member (member_name, drivers_license, email, member_address) VALUES ('Fake Member_1', 'ABC1234', 'fake_member_1@techelevator.com', '1275 Kinnear Road');" +
                        "SELECT CAST(SCOPE_IDENTITY() as int);", conn);

                    memberId = (int)cmd.ExecuteScalar();

                    cmd = new SqlCommand("INSERT INTO tool(brand, tool_name, description, checked_out) VALUES('fakeBrand1', 'TestTool1', 'Fake Description1', 0);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int);", conn);

                    tool1Id = (int)cmd.ExecuteScalar();

                    cmd = new SqlCommand("INSERT INTO tool(brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date) VALUES('fakeBrand2', 'TestTool2', 'Fake Description2', 1, @current_borrower, '2018-12-11', '2018-12-18');" +
                        "SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                    cmd.Parameters.AddWithValue("@current_borrower", memberId);

                    tool2Id = (int)cmd.ExecuteScalar();
                }

                Assert.IsTrue(_toolDal.ChangeCheckedOutStatus(tool1Id, true));
                Assert.IsTrue(_toolDal.ChangeCheckedOutStatus(tool2Id, false));

                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE tool SET current_borrower = @current_borrower WHERE id = @id;", conn);
                    cmd.Parameters.AddWithValue("@current_borrower", memberId);
                    cmd.Parameters.AddWithValue("@id", tool1Id);

                    cmd.ExecuteNonQuery();              
                }

                Assert.AreEqual("TestTool1", _toolDal.GetTools(true)[0].ToolName);
                Assert.AreEqual("TestTool2", _toolDal.GetTools(false)[0].ToolName);
            }

            [TestMethod]
            public void CheckOutTest()
            {
                int member1Id;
                int tool1Id;
                int tool2Id;

                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO member (member_name, drivers_license, email, member_address) VALUES ('Fake Member_1', 'ABC1234', 'fake_member_1@techelevator.com', '1275 Kinnear Road');" +
                        "SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                    member1Id = (int)cmd.ExecuteScalar();
                   
                    cmd = new SqlCommand("INSERT INTO member (member_name, drivers_license, email, member_address) VALUES ('Fake Member_2', 'DEF5678', 'fake_member_2@techelevator.com', '1275 Kinnear Road');", conn);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("INSERT INTO tool(brand, tool_name, description, checked_out) VALUES('fakeBrand1', 'TestTool1', 'Fake Description1', 1);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int);", conn);

                    tool1Id = (int)cmd.ExecuteScalar();

                    cmd = new SqlCommand("INSERT INTO tool(brand, tool_name, description, checked_out) VALUES('fakeBrand2', 'TestTool2', 'Fake Description2', 1);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int);", conn);

                    tool2Id = (int)cmd.ExecuteScalar();
                }

                Tool tool1 = new Tool
                {
                    Id = tool1Id,
                    Brand = "fakeBrand1",
                    ToolName = "TestTool1",
                    Description = "Fake Description1",
                    CheckedOut = true,
                    CurrentBorrowerName = "Fake Member_1",
                    DateBorrowed = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7)
                };

                Tool tool2 = new Tool
                {
                    Id = tool2Id,
                    Brand = "fakeBrand2",
                    ToolName = "TestTool2",
                    Description = "Fake Description2",
                    CheckedOut = true,
                    CurrentBorrowerName = "Fake Member_2",
                    DateBorrowed = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7)
                };

                IList<Tool> tools = new List<Tool>
                {
                    tool1,
                    tool2
                };

                Cart cart = new Cart
                {
                    Tools = tools
                };

                Assert.IsTrue(_toolDal.CheckOut(cart));
                _toolDal.CheckOut(cart);
                Assert.AreEqual(member1Id, _toolDal.GetTools(true)[0].CurrentBorrowerId);
            }
        }
    }
}