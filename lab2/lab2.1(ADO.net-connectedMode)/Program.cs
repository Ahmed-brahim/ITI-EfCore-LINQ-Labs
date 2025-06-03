using Microsoft.Data.SqlClient;
using System.Transactions;

namespace lab2._1_ADO.net_connectedMode_
{
    public class Program
    {
        static void Main(string[] args)
        {
            string cs = new string("Data Source=LAPTOP-DC6PHRTK\\SQLEXPRESS;Initial Catalog=WFormDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            SqlConnection Connection = new SqlConnection(cs);
            SqlCommand sqlCommandSelect = new SqlCommand("select * from department", Connection);
            SqlCommand sqlCommandInsert = new SqlCommand("insert into department values (@id, @name)", Connection);
            SqlCommand sqlCommandDelete = new SqlCommand("delete from department where dept_id = @id", Connection);
            SqlCommand sqlCommandUpdate = new SqlCommand("Update department set dept_name = @name where dept_id = @id", Connection);

            Connection.Open();
            printTable(sqlCommandSelect);
            
            Console.WriteLine("Try to insert new value");
            Console.Write("Department Name:");
            string name = Console.ReadLine();
            bool flag = false;
            int id = 0;
            do
            {
                Console.Write("Id:");
                flag = int.TryParse(Console.ReadLine(), out id);
            }
            while (!flag);

            //Execute insert
            sqlCommandInsert.Parameters.AddWithValue("name", name);
            sqlCommandInsert.Parameters.AddWithValue("id", id);
            sqlCommandInsert.ExecuteNonQuery();

            Console.WriteLine("/n----------after insert---------");

            //read
            printTable(sqlCommandSelect);

            //delete
            Console.WriteLine("try to delete from dept table");
            do
            {
                Console.Write("Id:");
                flag = int.TryParse(Console.ReadLine(), out id);
            }
            while (!flag);
            sqlCommandDelete.Parameters.AddWithValue("id", id);
            sqlCommandDelete.ExecuteNonQuery ();
            Console.WriteLine("\n--------------after delete------------------");
            printTable(sqlCommandSelect);

            //update
            Console.WriteLine("Try to update old value");
            Console.Write("New Department Name:");
             name = Console.ReadLine();
             flag = false;
             id = 0;
            do
            {
                Console.Write("Old Id:");
                flag = int.TryParse(Console.ReadLine(), out id);
            }
            while (!flag);

            //Execute insert
            sqlCommandUpdate.Parameters.AddWithValue("name", name);
            sqlCommandUpdate.Parameters.AddWithValue("id", id);
            sqlCommandUpdate.ExecuteNonQuery();

            Console.WriteLine("\n--------------after update------------------");
            printTable(sqlCommandSelect);

            Connection.Close();
        }
        static void printTable(SqlCommand command)
        {
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"id: {reader.GetInt32(0)}    name: {reader.GetString(1)}");
            }
            Console.WriteLine();
            reader.Close();
        }
    }
}
