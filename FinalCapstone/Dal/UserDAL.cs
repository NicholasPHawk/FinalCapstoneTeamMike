using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Dal
{
    public class UserDAL : IUserDAL
    {
        private readonly string connectionString;

        public UserDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public User GetUser(string email)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM member WHERE email = @email;", conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    User user = new User();
                    while (reader.Read())
                    {
                        user.Id = Convert.ToInt32(reader["id"]);
                        user.Name = Convert.ToString(reader["brand"]);
                        user.DriversLicense = Convert.ToString(reader["tool_name"]);
                        user.Username = Convert.ToString(reader["description"]);
                        user.Password = Convert.ToString(reader["description"]);
                        user.Salt = Convert.ToString(reader["description"]);
                        user.Email = Convert.ToString(reader["description"]);
                    }
                    return user;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public void RegisterUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO member (member_name, drivers_license, username, password, email) VALUES (@member_name, @drivers_license, @username, @password, @email)", conn);
                    cmd.Parameters.AddWithValue("@member_name", user.Email);
                    cmd.Parameters.AddWithValue("@Drivers_license", user.Email);
                    cmd.Parameters.AddWithValue("@username", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Email);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
