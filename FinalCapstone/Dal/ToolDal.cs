using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace FinalCapstone.Dal
{
    public class ToolDal : IToolDal
    {
        private readonly string connectionString;

        public ToolDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Tool> GetTools(bool checkedOut)
        {
            return GetAllTools(checkedOut);
        }

        public Tool GetToolDetails(int id, bool checkedOut)
        {
            return GetAllTools(checkedOut).FirstOrDefault(t => t.Id == id);
        }

        public IList<Tool> GetAllTools(bool checkedOut)
        {
            IList<Tool> tools = new List<Tool>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "";

                if (checkedOut == true)
                {
                    sql = "SELECT m.member_name, t.* FROM member m, tool t WHERE m.id = t.current_borrower AND checked_out = 1;";
                }
                else
                {
                    sql = "SELECT * FROM tool WHERE checked_out = 0;";
                }

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Tool tool = new Tool();
                    tool.Id = Convert.ToInt32(reader["id"]);
                    tool.Brand = Convert.ToString(reader["brand"]);
                    tool.ToolName = Convert.ToString(reader["tool_name"]);
                    tool.Description = Convert.ToString(reader["description"]);
                    tool.CheckedOut = Convert.ToBoolean(reader["checked_out"]);
                    if (checkedOut == true)
                    {
                        tool.CurrentBorrowerName = Convert.ToString(reader["member_name"]);
                    }
                    if ((reader["current_borrower"] is DBNull))
                    {
                        tool.CurrentBorrowerId = 0;
                    }
                    else
                    {
                        tool.CurrentBorrowerId = Convert.ToInt32(reader["current_borrower"]);
                    }
                    if ((reader["date_borrowed"] is DBNull))
                    {
                        tool.DateBorrowed = DateTime.MinValue;
                    }
                    else
                    {
                        tool.DateBorrowed = Convert.ToDateTime(reader["date_borrowed"]);
                    }
                    if ((reader["due_date"] is DBNull))
                    {
                        tool.DueDate = DateTime.MinValue;
                    }
                    else
                    {
                        tool.DueDate = Convert.ToDateTime(reader["due_date"]);
                    }

                    tools.Add(tool);
                }
            }

            return tools;
        }

        public IList<ToolLoanRecordSearchModel> GetLoanRecords(string searchOption, string searchString)
        {
            IList<ToolLoanRecordSearchModel> tools = new List<ToolLoanRecordSearchModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (searchOption == "Name")
                {
                    SqlCommand cmd = new SqlCommand("SELECT m.member_name,  m.drivers_license, t.* FROM member m, tool t WHERE m.id = t.current_borrower AND checked_out = 1 AND m.member_name LIKE @member_name;", conn);
                    cmd.Parameters.AddWithValue("@member_name", "%" + searchString + "%");
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ToolLoanRecordSearchModel tool = new ToolLoanRecordSearchModel();
                        tool.Id = Convert.ToInt32(reader["id"]);
                        tool.Brand = Convert.ToString(reader["brand"]);
                        tool.ToolName = Convert.ToString(reader["tool_name"]);
                        tool.Description = Convert.ToString(reader["description"]);
                        tool.CheckedOut = Convert.ToBoolean(reader["checked_out"]);
                        tool.CurrentBorrowerName = Convert.ToString(reader["member_name"]);
                        tool.LicenseNumber = Convert.ToString(reader["drivers_license"]);
                        tool.CurrentBorrowerId = Convert.ToInt32(reader["current_borrower"]);
                        tool.DateBorrowed = Convert.ToDateTime(reader["date_borrowed"]);
                        tool.DueDate = Convert.ToDateTime(reader["due_date"]);
                        tools.Add(tool);
                    }
                }

                if (searchOption == "License")
                {
                    SqlCommand cmd = new SqlCommand("SELECT m.member_name, m.drivers_license, t.* FROM member m, tool t WHERE m.id = t.current_borrower AND checked_out = 1 AND m.drivers_license = @drivers_license;", conn);
                    cmd.Parameters.AddWithValue("@drivers_license", "%" + searchString + "%");
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ToolLoanRecordSearchModel tool = new ToolLoanRecordSearchModel();
                        tool.Id = Convert.ToInt32(reader["id"]);
                        tool.Brand = Convert.ToString(reader["brand"]);
                        tool.ToolName = Convert.ToString(reader["tool_name"]);
                        tool.Description = Convert.ToString(reader["description"]);
                        tool.CheckedOut = Convert.ToBoolean(reader["checked_out"]);
                        tool.CurrentBorrowerName = Convert.ToString(reader["member_name"]);
                        tool.LicenseNumber = Convert.ToString(reader["drivers_license"]);
                        tool.CurrentBorrowerId = Convert.ToInt32(reader["current_borrower"]);
                        tool.DateBorrowed = Convert.ToDateTime(reader["date_borrowed"]);
                        tool.DueDate = Convert.ToDateTime(reader["due_date"]);
                        tools.Add(tool);
                    }
                }

                if (searchOption == "Tool Number")
                {
                    int x;
                    SqlCommand cmd = new SqlCommand("SELECT m.member_name, m.drivers_license, t.* FROM member m, tool t WHERE m.id = t.current_borrower AND checked_out = 1 AND t.id = @id;", conn);
                    try
                    {
                        x = Int32.Parse(searchString);
                    }
                    catch (Exception ex)
                    {
                        x = 0;
                    }
                    cmd.Parameters.AddWithValue("@id", x);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ToolLoanRecordSearchModel tool = new ToolLoanRecordSearchModel();
                        tool.Id = Convert.ToInt32(reader["id"]);
                        tool.Brand = Convert.ToString(reader["brand"]);
                        tool.ToolName = Convert.ToString(reader["tool_name"]);
                        tool.Description = Convert.ToString(reader["description"]);
                        tool.CheckedOut = Convert.ToBoolean(reader["checked_out"]);
                        tool.CurrentBorrowerName = Convert.ToString(reader["member_name"]);
                        tool.LicenseNumber = Convert.ToString(reader["drivers_license"]);
                        tool.CurrentBorrowerId = Convert.ToInt32(reader["current_borrower"]);
                        tool.DateBorrowed = Convert.ToDateTime(reader["date_borrowed"]);
                        tool.DueDate = Convert.ToDateTime(reader["due_date"]);
                        tools.Add(tool);
                    }
                }
            }

            return tools;
        }

        public IList<User> GetUsers()
        {
            IList<User> users = new List<User>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT member_name FROM member;", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User();
                    user.Name = Convert.ToString(reader["member_name"]);
                    users.Add(user);
                }
            }

            return users;
        }

        public bool ChangeCheckedOutStatus(int id, bool checkedOut)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE tool SET checked_out = @checked_out WHERE id = @id;", conn);
                cmd.Parameters.AddWithValue("@checked_out", checkedOut);
                cmd.Parameters.AddWithValue("@id", id);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public bool CheckOut(Cart cart)
        {
            bool success = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql1 = "SELECT id FROM member WHERE member_name = @member_name;";
                    string sql2 = "UPDATE tool SET current_borrower = @current_borrower, date_borrowed = @date_borrowed, " +
                        "due_date = @due_date WHERE id = @id;";

                    foreach (Tool tool in cart.Tools)
                    {
                        SqlCommand cmd1 = new SqlCommand(sql1, conn);
                        cmd1.Parameters.AddWithValue("@member_name", tool.CurrentBorrowerName);
                        int memberId = (int)cmd1.ExecuteScalar();

                        SqlCommand cmd2 = new SqlCommand(sql2, conn);
                        cmd2.Parameters.AddWithValue("@current_borrower", memberId);
                        cmd2.Parameters.AddWithValue("@date_borrowed", tool.DateBorrowed);
                        cmd2.Parameters.AddWithValue("@due_date", tool.DueDate);
                        cmd2.Parameters.AddWithValue("@id", tool.Id);
                        cmd2.ExecuteNonQuery();
                    }
                }
                success = true;
            }
            catch
            {
            }

            return success;
        }

        public IList<Tool> RemoveAToolList()
        {
            IList<Tool> allTools = new List<Tool>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tool;", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Tool tool = new Tool();
                    tool.Id = Convert.ToInt32(reader["id"]);
                    tool.Brand = Convert.ToString(reader["brand"]);
                    tool.ToolName = Convert.ToString(reader["tool_name"]);
                    allTools.Add(tool);
                }
                return allTools;
            }
        }

        public void RemoveATool(Tool tool)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM tool WHERE id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", tool.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public bool AddTool(Tool tool)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO tool (brand, tool_name, description, checked_out) VALUES (@brand, @tool_name, @description, @checked_out);", conn);
                cmd.Parameters.AddWithValue("@brand", tool.Brand);
                cmd.Parameters.AddWithValue("@tool_name", tool.ToolName);
                cmd.Parameters.AddWithValue("@description", tool.Description);
                cmd.Parameters.AddWithValue("@checked_out", 0);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
