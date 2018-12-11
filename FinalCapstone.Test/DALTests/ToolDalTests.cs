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
                    const string sql =
                        @"INSERT INTO tool VALUES ('TestTool1', 'Fake Description1', 0, 'fakeBrand1');
                          INSERT INTO tool VALUES ('TestTool2', 'Fake Description2', 1, 'fakeBrand2');
                          INSERT INTO tool VALUES ('TestTool3', 'Fake Description3', 1, 'fakeBrand3');";

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = sql;

                    conn.Open();
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
                    const string sql =
                        @"INSERT INTO tool VALUES ('TestTool1', 'Fake Description1', 0, 'fakeBrand1');
                          INSERT INTO tool VALUES ('TestTool2', 'Fake Description2', 1, 'fakeBrand2');
                          INSERT INTO tool VALUES ('TestTool3', 'Fake Description3', 1, 'fakeBrand3');";

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = sql;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                    var checkedOutTools = _toolDal.GetTools(false);
                    Assert.AreEqual(1, checkedOutTools.Count);
            }
        }
    }
}

