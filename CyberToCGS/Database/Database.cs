using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS.Database
{
   public class Database
    {
        string db_online = @"server = 192.168.0.83; database = DB_ONLINE_CG; user = sa; password = ABC123abc$; ";
        string localDb = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=testDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string connectionString = null;

        SqlConnection connection;
        SqlCommand command;
        SqlDataReader dataReader;
        String Sql;
        public Database(string value) {
            if (value == "localDB")
            {
                connectionString = localDb;
            }
            if(value == "DB_ONLINE_CG")
            {
                connectionString =  db_online;
            }
        }
        private static Database _database;
        private static readonly object _lock = new object();
        public string Value { get; set; }
        public static Database GetInstance(string value)
        {
            if(_database == null)
            {
                lock (_lock)
                {
                    if(_database == null)
                    {
                        _database = new Database(value);
                        _database.Value = value;

                    }
                }
               
            }
           
            return _database;
        }
        public void GetUser()
        {
           
             Sql = "select id,name from [dbo].[user]";
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql,connection);
                dataReader = command.ExecuteReader();
                Console.WriteLine("-------Get All User ----------- ");
                while (dataReader.Read())
                {
                   
                    Console.WriteLine(dataReader.GetValue(1));
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("getUser error" + ex.Message.ToString());
            }
        }
        public void GetUser(int id)
        {

             Sql = "SELECT id,name FROM  [dbo].[user] "
                         +" WHERE id = @id;";
            connection = new SqlConnection(connectionString);

            Console.WriteLine("---------Get User By Parameter :" + id.ToString() + "-------");
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);
                
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@ID"].Value = id;

                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    
                    Console.WriteLine(dataReader.GetValue(1));
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("getUser error" + ex.Message.ToString());
            }
        }
        public void SetConnection(string value)
        {
            if(value == "DB_ONLINE_CG")
            {
                connectionString = db_online;
            }
        }
        public void GetT01_Request_Online()
        {
            Sql = "select T01Online_ID, T01Send_Date, T01Name_Thai, T01Surname_Thai, T01Last_Status "
                + " from[dbo].[T01_Request_Online] "
                + " where t01last_status >= '010' "
                + " and t01last_status <= '110' " ;
            /*
             *
             *and T01Send_Date >= '{0}'
             *and T01Send_Date <= '{1}
             *order by t01online_id desc", strFrom, strTo); 
             */
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);
                dataReader = command.ExecuteReader();
                Console.WriteLine("------- GetT01_Request_Online() ----------- ");
                while (dataReader.Read())
                {

                    Console.WriteLine(dataReader.GetValue(1) + "-"  + dataReader.GetValue(2) );
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("getUser error" + ex.Message.ToString());
            }
        }
    }
}
