using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using FinalCapstone.Dal;
using System.Transactions;
using System.Linq;




namespace FinalCapstone.Test.DALTests
{
    [TestClass]
    public class ToolDalTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\sqlexpress01;Initial Catalog = toolDB; Integrated Security = True";
        private int Id;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                cmd = new SqlCommand("INSERT INTO tool VALUES ('TestTool', 'Fake Description', '0', fakeBrand');", conn);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO tool VALUES ('TestTool2', 'Fake Description2', '1', fakeBrand2');", conn);
                cmd.ExecuteNonQuery();

            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void Should_Be_Able_To_Be_Checked_Out()
        {
            IList<Tool> tools = new List<Tool>();
            IToolDal _toolDal = new ToolDal(@"Data Source=.\sqlexpress01;Initial Catalog = toolDB; Integrated Security = True");

        }

        [TestMethod]
        public void Should_Not_Be_Able_To_Be_Checked_Out()
        {

        }



    }
}

