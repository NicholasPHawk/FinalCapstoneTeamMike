using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using Microsoft.Extensions.Configuration;

namespace FinalCapstone.Test.DALTests
{
    public abstract class DatabaseTest
    {
        private TransactionScope _transaction;

        [TestInitialize]
        public void DatabaseSetup()
        {
            _transaction = new TransactionScope();
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            using (var connection = new SqlConnection(toolDBConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText =
                    @"DELETE FROM tool; 
                      DELETE FROM member;";

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void DatabaseCleanup()
        {
            _transaction.Dispose();
        }

        protected IConfiguration Configuration { get; private set; }

        protected string toolDBConnectionString
        {
            get
            {
                return Configuration["ConnectionStrings:Default"];
            }
        }
    }
}
