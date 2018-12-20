using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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
                       @"INSERT INTO tool (brand, tool_name, description, checked_out, image_name) VALUES ('fakeBrand1', 'TestTool1', 'Fake Description1', 0, 'Fake Image1');
                         INSERT INTO tool (brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date, image_name) VALUES ('fakeBrand2', 'TestTool2', 'Fake Description2', 1, @current_borrower, '2018-12-11', '2018-12-18', 'Fake Image2');
                         INSERT INTO tool (brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date, image_name) VALUES ('fakeBrand3', 'TestTool3', 'Fake Description3', 1, @current_borrower, '2018-12-11', '2018-12-18', 'Fake Image3');";

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
                       @"INSERT INTO tool (brand, tool_name, description, checked_out, image_name) VALUES ('fakeBrand1', 'TestTool1', 'Fake Description1', 0, 'Fake Image1');
                         INSERT INTO tool (brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date, image_name) VALUES ('fakeBrand2', 'TestTool2', 'Fake Description2', 1, @current_borrower, '2018-12-11', '2018-12-18', 'Fake Image2');
                         INSERT INTO tool (brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date, image_name) VALUES ('fakeBrand3', 'TestTool3', 'Fake Description3', 1, @current_borrower, '2018-12-11', '2018-12-18', 'Fake Image3');";

                    cmd = conn.CreateCommand();
                    cmd.CommandText = sql2;
                    cmd.Parameters.AddWithValue("@current_borrower", memberId);

                    cmd.ExecuteNonQuery();
                }
                var checkedOutTools = _toolDal.GetTools(true);
                Assert.AreEqual(2, checkedOutTools.Count);
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
                       @"INSERT INTO tool (brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date, image_name) VALUES ('fakeBrand2', 'TestTool2', 'Fake Description2', 0, @current_borrower, '2018-12-11', '2018-12-18', 'Fake Image2'); SELECT CAST(SCOPE_IDENTITY() as int);";

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

                    cmd = new SqlCommand("INSERT INTO tool(brand, tool_name, description, checked_out, image_name) VALUES('fakeBrand1', 'TestTool1', 'Fake Description1', 0, 'Fake Image1');" +
                        "SELECT CAST(SCOPE_IDENTITY() as int);", conn);

                    tool1Id = (int)cmd.ExecuteScalar();

                    cmd = new SqlCommand("INSERT INTO tool(brand, tool_name, description, checked_out, current_borrower, date_borrowed, due_date, image_name) VALUES('fakeBrand2', 'TestTool2', 'Fake Description2', 1, @current_borrower, '2018-12-11', '2018-12-18', 'Fake Image2');" +
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

                    cmd = new SqlCommand("INSERT INTO tool(brand, tool_name, description, checked_out, image_name) VALUES('fakeBrand1', 'TestTool1', 'Fake Description1', 1, 'Fake Image1');" +
                        "SELECT CAST(SCOPE_IDENTITY() as int);", conn);

                    tool1Id = (int)cmd.ExecuteScalar();

                    cmd = new SqlCommand("INSERT INTO tool(brand, tool_name, description, checked_out, image_name) VALUES('fakeBrand2', 'TestTool2', 'Fake Description2', 1, 'Fake Image2');" +
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
                    DueDate = DateTime.Now.AddDays(7),
                    ImageName = "Fake Image1"
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
                    DueDate = DateTime.Now.AddDays(7),
                    ImageName = "Fake Image2"
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

            [TestMethod]
            public void RemoveAToolTest()
            {
                Tool tool1 = new Tool();
                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO tool(brand, tool_name, description, checked_out, image_name) VALUES('fakeBrand1', 'TestTool1', 'Fake Description1', 0, 'Fake Image1');" +
                        "SELECT CAST(SCOPE_IDENTITY() as int);", conn);

                    int tool1Id = (int)cmd.ExecuteScalar();

                    tool1.Id = tool1Id;
                    tool1.Brand = "fakeBrand1";
                    tool1.ToolName = "TestTool1";
                    tool1.Description = "Fake Description1";
                    tool1.CheckedOut = false;
                    tool1.ImageName = "Fake Image1";
                }
                Assert.IsTrue(_toolDal.RemoveATool(tool1));
            }

            [TestMethod]
            public void AddToolTest()
            {
                Tool tool = new Tool();
                tool.Brand = "NO";
                tool.ToolName = "ONE";
                tool.Description = "CARES";
                tool.CheckedOut = false;
                tool.ImageName = "Fake Image";

                Assert.IsTrue(_toolDal.AddTool(tool));
            }

            [TestMethod]
            public void RemoveAToolListTest()
            {
                IList<Tool> tools = new List<Tool>();
                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO tool(brand, tool_name, description, checked_out, image_name) VALUES('fakeBrand', 'TestTool', 'Fake Description', 0, 'Fake Image');", conn);
                    cmd.ExecuteNonQuery();
                }
                tools = _toolDal.RemoveAToolList();

                Assert.AreEqual(1, tools.Count);
            }
        }
    }
}