using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace DAL
{
    public static class DbManager
    {
        //connection
        static SqlConnection connection;
        //command
        static SqlCommand cmd;
        //datatable
        static DataTable table;
        static DbManager()
        {
            connection = new SqlConnection("Data Source=LAPTOP-DC6PHRTK\\SQLEXPRESS;Initial Catalog=WFormDb;Integrated Security=True;Trust Server Certificate=True");
            cmd = new SqlCommand("", connection);
        }
        public static DataTable ExecuteQuery(string cmdString)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = cmdString;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public static int ExecuteNonQuery(string cmdString)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = cmdString;
            connection?.Open();
            int AffectedRows = cmd.ExecuteNonQuery();
            connection?.Close();
            return AffectedRows;
        }
    }
}
