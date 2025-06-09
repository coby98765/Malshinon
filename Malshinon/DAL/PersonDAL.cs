using Malshinon.DB;
using Malshinon.models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Malshinon.models
    {
    internal class PersonDAL
        {
        private MySqlData _sqlData;
        public PersonDAL(MySqlData connData)
            {
            _sqlData = connData;
            }

        //CRUD Methods
        //Create
        public void CreatePerson(Person person)
            {
            string query = @"INSERT INTO people
                           (secret_code,first_name,last_name,type,num_reports,num_mentions)
                            VALUES(@SecretCode,@FirstName,@LastName,@Type,@NumReports,@NumMentions)";
            MySqlCommand cmd = new MySqlCommand(query, _sqlData.GetConnection());
            try
                {
                cmd.Parameters.AddWithValue("@SecretCode", person.SecretCode);
                cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                cmd.Parameters.AddWithValue("@LastName", person.LastName);
                cmd.Parameters.AddWithValue("@Type", person.Type);
                cmd.Parameters.AddWithValue("@NumReports", person.NumReports);
                cmd.Parameters.AddWithValue("@NumMentions", person.NumMentions);

                int response = cmd.ExecuteNonQuery();
                Console.WriteLine(response > 0
                    ? "Person Added."
                    : "Person was not Added.");
                }
            catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                }
            finally
                {
                _sqlData.CloseConnection();
                }
            }

        }
    }
