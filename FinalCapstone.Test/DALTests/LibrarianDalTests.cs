using FinalCapstone.Dal;
using FinalCapstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FinalCapstone.Test.DALTests
{
    [TestClass]
    public class LibrarianDalTests : DatabaseTest
    {
        private ILibrarianDal _librarianDal;
        private IToolDal _toolDal;

        [TestInitialize]
        public void Setup()
        {
            _librarianDal = new LibrarianDal(toolDBConnectionString);
            _toolDal = new ToolDal(toolDBConnectionString);
        }

        [TestClass]
        public class LibrarianTests : LibrarianDalTests
        {
            [TestMethod]
            public void GetLibrarianTest()
            {
                int librarianId;

                using (SqlConnection conn = new SqlConnection(toolDBConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO librarian (username, password_value, salt) VALUES ('Fake Librarian', 'Password', 'Salt');" +
                        "SELECT CAST(SCOPE_IDENTITY() as int);", conn);

                    librarianId = (int)cmd.ExecuteScalar();
                }

                Assert.AreEqual(librarianId, _librarianDal.GetLibrarian("Fake Librarian").Id);
            }

            [TestMethod]
            public void RegisterUserTest()
            {
                User user = new User
                {
                    Name = "Fake Member_1",
                    DriversLicense = "ABC1234",
                    Email = "fake_member_1@techelevator.com",
                    Address = "1275 Kinnear Road"
                };

                Assert.IsTrue(_librarianDal.RegisterUser(user));

                IList<User> users = _toolDal.GetUsers();
                Assert.AreEqual(1, users.Count);
                Assert.AreEqual("Fake Member_1", users[0].Name);
            }

            [TestMethod]
            public void RegisterLibrarianTest()
            {
                Librarian librarian = new Librarian
                {
                    Username = "Fake Librarian",
                    Password = "Password",
                    Salt = "Salt"
                };

                Assert.IsTrue(_librarianDal.RegisterLibrarian(librarian));
                Assert.AreEqual("Password", _librarianDal.GetLibrarian("Fake Librarian").Password);
            }
        }
    }
}
