using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FinalCapstone.Models;

namespace FinalCapstone.Dal
{
    public class ToolDal : IToolDal
    {
        private readonly string connectionString;

        public ToolDal(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Tool> GetTools()
        {
            return GetAllTools();
        }

        public Tool GetToolDetails(int id)
        {
            return GetAllTools().FirstOrDefault(t => t.Id == id);
        }

        public IList<Tool> GetAllTools()
        {
            IList<Tool> tools = new List<Tool>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tool", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Tool tool = new Tool();
                    tool.Id = Convert.ToInt32(reader["id"]);
                    tool.ToolName = Convert.ToString(reader["name"]);
                    tool.Brand = Convert.ToString(reader["brand"]);
                    tool.Description = Convert.ToString(reader["description"]);
                    tool.CheckedOut = Convert.ToBoolean(reader["checked_out"]);
                    tools.Add(tool);
                }
            }
            return tools;
        }
    }
}
