
using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
