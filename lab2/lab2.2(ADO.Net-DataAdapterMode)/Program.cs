using Microsoft.Data.SqlClient;
using System.Data;

namespace lab2._2_ADO.Net_DataAdapterMode_
{
    public class Program
    {
        static void Main(string[] args)
        {
            string cs = "Data Source=LAPTOP-DC6PHRTK\\SQLEXPRESS;Initial Catalog=WFormDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            SqlConnection connection = new SqlConnection(cs);

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM department", connection);
            DataTable dt = new DataTable();

            // CommandBuilder auto-generates InsertCommand, UpdateCommand, DeleteCommand
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            // Fill DataTable 
            adapter.Fill(dt);

            Console.WriteLine("---- Initial Data ----");
            printTable(dt);

            // -------- INSERT --------
            Console.WriteLine("Try to insert new value");
            Console.Write("Department Name: ");
            string name = Console.ReadLine();

            int id;
            while (true)
            {
                Console.Write("Id: ");
                if (int.TryParse(Console.ReadLine(), out id)) break;
            }

            DataRow newRow = dt.NewRow();
            newRow["dept_id"] = id;
            newRow["dept_name"] = name;
            dt.Rows.Add(newRow);

            adapter.Update(dt); // Push changes to DB
            Console.WriteLine("\n---- After Insert ----");
            reloadData(adapter, dt);

            Console.WriteLine("Try to delete from dept table");
            while (true)
            {
                Console.Write("Id: ");
                if (int.TryParse(Console.ReadLine(), out id)) break;
            }

            // Find and delete row
            DataRow rowToDelete = dt.Rows.Cast<DataRow>().FirstOrDefault(row => (int)row["dept_id"] == id);

            rowToDelete.Delete();
            adapter.Update(dt);
            Console.WriteLine("\n---- After Delete ----");
            reloadData(adapter, dt);
            

            // -------- UPDATE --------
            Console.WriteLine("Try to update a record");
            Console.Write("New Department Name: ");
            name = Console.ReadLine();
            while (true)
            {
                Console.Write("Old Id: ");
                if (int.TryParse(Console.ReadLine(), out id)) break;
            }

            // Find and update
            DataRow? rowToUpdate = dt.Rows.Cast<DataRow>().FirstOrDefault(row => (int)row["dept_id"] == id);
            if (rowToUpdate != null)
            {
                rowToUpdate["dept_name"] = name;
                adapter.Update(dt);
                Console.WriteLine("\n---- After Update ----");
                reloadData(adapter, dt);
            }

            connection.Close();
        }

        static void printTable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                    Console.WriteLine($"id: {row["dept_id"]}   name: {row["dept_name"]}");
            }
            Console.WriteLine();
        }

        static void reloadData(SqlDataAdapter adapter, DataTable table)
        {
            table.Clear();
            adapter.Fill(table);
            printTable(table);
        }
    }
}
