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

        public bool CheckToolAvailability(int id)
        {
            bool checkedOut = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT checked_out FROM tool WHERE id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", id);

                checkedOut = (bool)cmd.ExecuteScalar();
            }

            return checkedOut;
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
                    cmd.Parameters.AddWithValue("@drivers_license", searchString);
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
                    SqlCommand cmd = new SqlCommand("SELECT m.member_name, m.drivers_license, t.* FROM member m, tool t WHERE m.id = t.current_borrower AND checked_out = 1 AND t.id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", Int32.Parse(searchString));
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
    }
}
