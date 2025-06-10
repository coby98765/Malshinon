using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Malshinon.DB
    {
    public class MySqlData
        {
        static string connectionString = "Server=localhost;Port=3306;DataBase=malshinon_db;User=root;Password='';";
        private MySqlConnection _conn;

        public void Setup()
            {
            var conn = new MySqlConnection(connectionString);
            _conn = conn;
            try
                {
                conn.Open();
                Console.WriteLine("Connected to: 'Malshinon DB'");
                conn.Close();
                Console.WriteLine("Connection Was Closed.");

                }
            catch (MySqlException ex)
                {
                Console.WriteLine($"Error connecting: {ex.Message}");
                }
            }

        public MySqlConnection GetConnection()
            {
            _conn.Open();
            Console.WriteLine("Connection Opened.");
            return _conn;
            }

        public void CloseConnection()
            {
            _conn.Close();
            Console.WriteLine("Connection Closed.");
            }
        }
    }
