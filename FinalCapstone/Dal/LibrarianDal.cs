using FinalCapstone.Models;
using System;
using System.Data.SqlClient;

namespace FinalCapstone.Dal
{
    public class LibrarianDal : ILibrarianDal
    {
        private readonly string connectionString;

        public LibrarianDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Librarian GetLibrarian(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM librarian WHERE username = @username;", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Librarian librarian = new Librarian();
                    while (reader.Read())
                    {
                        librarian.Id = Convert.ToInt32(reader["id"]);
                        librarian.Username = Convert.ToString(reader["username"]);
                        librarian.Password = Convert.ToString(reader["password_value"]);
                        librarian.Salt = Convert.ToString(reader["salt"]);
                    }
                    return librarian;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool RegisterUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO member (member_name, drivers_license, email, member_address) VALUES (@member_name, @drivers_license,  @email, @member_address)", conn);
                    cmd.Parameters.AddWithValue("@member_name", user.Name);
                    cmd.Parameters.AddWithValue("@drivers_license", user.DriversLicense);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@member_address", user.Address);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool RegisterLibrarian(Librarian librarian)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO librarian (username, password_value, salt) VALUES (@username, @password_value, @salt)", conn);
                    cmd.Parameters.AddWithValue("@username", librarian.Username);
                    cmd.Parameters.AddWithValue("@password_value", librarian.Password);
                    cmd.Parameters.AddWithValue("@salt", librarian.Salt);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
