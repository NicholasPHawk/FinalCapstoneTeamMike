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
            public void GetToolsCheckedOut()
            {
                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    const string sql1 = "INSERT INTO member (member_name, drivers_license) VALUES ('Han Solo', 'BO085123'); SELECT CAST(SCOPE_IDENTITY() as int);";

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
            public void GetToolsCheckedIn()
            {
                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    const string sql1 = "INSERT INTO member (member_name, drivers_license) VALUES ('Han Solo', 'BO085123'); SELECT CAST(SCOPE_IDENTITY() as int);";

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
        }
    }
}

