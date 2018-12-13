using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Dal
{
    public class UserDal : IUserDal
    {
        private readonly string connectionString;

        public UserDal(string connectionString)
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
                        user.Name = Convert.ToString(reader["member_name"]);
                        user.DriversLicense = Convert.ToString(reader["drivers_license"]);
                        user.Username = Convert.ToString(reader["username"]);
                        user.Password = Convert.ToString(reader["password_value"]);
                        user.Salt = "?";
                        user.Email = Convert.ToString(reader["email"]);
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
                    SqlCommand cmd = new SqlCommand("INSERT INTO member (member_name, drivers_license, email, username, password_value) VALUES (@member_name, @drivers_license,  @email, @username, @password_value)", conn);
                    cmd.Parameters.AddWithValue("@member_name", user.Name);
                    cmd.Parameters.AddWithValue("@drivers_license", user.DriversLicense);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password_value", user.Password);
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
