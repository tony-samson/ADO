using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var select = new act();
            select.DisplayTable(new Connection().GetConn());
            if (select.Insert(103, '19:08:54', '2005-07-08', 'hans', new Connection().GetConn()))
            { Console.WriteLine("Row inserted"); }
            else { Console.WriteLine("Insert failed"); }
            Console.ReadKey() ; 
        }
    }
    public interface IConn
    {    SqlConnection GetConn(); }
    class Connection:IConn
    {
        private readonly string Connections = @"Data Source=.\SQLEXPRESS;Initial Catalog=tonys;Integrated Security=True";
        public  SqlConnection GetConn() { return new SqlConnection(Connections); }
    } 
    class act {
        public void DisplayTable(SqlConnection conn) 
        {
            string querystring = "Select * from sample";
            conn.Open();
            SqlCommand cmd = new SqlCommand(querystring, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString());
            }
            conn.Close();
            Console.ReadKey();
        }
        public bool Insert(int id,string time,string date,string name, SqlConnection conn) 
        {
            var insert = "insert into sample values("+id + ",'" + time + "','" + date + "','" + name + "')";
            conn.Open();
            SqlCommand cmd = new SqlCommand(insert, conn);
            try { cmd.ExecuteNonQuery(); }
            catch { return false; }
            finally { conn.Close(); }
            return true;
        }
    }
}
