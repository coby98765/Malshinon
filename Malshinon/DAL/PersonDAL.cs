using Google.Protobuf.Compiler;
using Malshinon.DB;
using Malshinon.models;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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

        //format user into Person Model
        private Person PersonFormatter(MySqlDataReader data)
            {
            int id = data.GetInt32("id");
            string firstName = data.GetString("first_name");
            string lastName = data.GetString("last_name");
            string codeName = data.GetString("secret_code");
            string type = data.GetString("type");
            int numReports = data.GetInt32("num_reports");
            int numMentions = data.GetInt32("num_mentions");

            Person newPerson = new Person(firstName, lastName, codeName, type, numReports, numMentions, id);
            return newPerson;
            }


        //CRUD Methods
        //Create
        public Person CreatePerson(Person person)
            {
            string query = @"INSERT INTO people
                           (secret_code,first_name,last_name,type)
                            VALUES(@SecretCode,@FirstName,@LastName,@Type)";
            MySqlCommand cmd = new MySqlCommand(query, _sqlData.GetConnection());
            MySqlDataReader reader = null;

            try
                {
                cmd.Parameters.AddWithValue("@SecretCode", person.SecretCode);
                cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                cmd.Parameters.AddWithValue("@LastName", person.LastName);
                cmd.Parameters.AddWithValue("@Type", person.Type);
                reader = cmd.ExecuteReader();
                person = PersonFormatter(reader);
                //int response = cmd.ExecuteNonQuery();
                Console.WriteLine("Person Added.");
                }
            catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                return null;
                }
            finally
                {
                _sqlData.CloseConnection();
                }
            return person;
            }

        //Update
        public Person UpdatePerson(Person person)
            {
            string query = $"UPDATE people SET type = '{person.Type}',num_reports = '{person.NumReports}',num_mentions = '{person.NumMentions}' WHERE id = {person.ID};";
            MySqlCommand cmd = new MySqlCommand(query, _sqlData.GetConnection());
            MySqlDataReader reader = null;
            try
                {
                reader = cmd.ExecuteReader();
                person = PersonFormatter(reader);
                Console.WriteLine("Person Updated.");
                }
            catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                return null;
                }
            finally
                {
                _sqlData.CloseConnection();
                }
            return person;
            }
        //Read
        //Universal People Getter
        private List<Person> GetPeopleQuery(string queryParam)
            {
            List<Person> people = new List<Person>();
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            string query = $"SELECT * FROM people{queryParam};";
            try
                {
                MySqlConnection connection = _sqlData.GetConnection();
                cmd = new MySqlCommand(query, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    {
                    people.Add(PersonFormatter(reader));
                    }
                }
            catch (Exception ex)
                {
                Console.WriteLine($"Error while fetching Person: {ex.Message}");
                throw;
                }
            finally
                {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                _sqlData.CloseConnection();
                }
            return people;
            }

        //Get Person By ID
        public Person GetPersonById(int id) 
            {
            string query = $" WHERE people.id = {id} LIMIT 1";
            List<Person> people = GetPeopleQuery(query);
            if (people.Count > 0)
                {
                return people[0];
                }
            else
                {
                Console.WriteLine("Person not found.");
                return null;
                }
            }

            //Get Person By SecretCode
        public Person GetPersonBySecret(string secretCode) 
            {
            string query = $" WHERE people.secret_code = '{secretCode}'";
            List<Person> people = GetPeopleQuery(query);
            if(people.Count > 0)
                {
                return people[0];
                }
            else
                {
                Console.WriteLine("Person not found.");
                return null;
                }
            }

        //Get All People
        public List<Person> GetPeopleList() 
            {
            return GetPeopleQuery("");
            }

        //Delete
        public Person DeletePersonById(int id) 
            {
            string query = $"DELETE FROM people WHERE people.id = {id}";
            MySqlDataReader reader = null;
            Person person;

            try
                {
                MySqlCommand cmd = new MySqlCommand(query, _sqlData.GetConnection());
                reader = cmd.ExecuteReader();
                person = PersonFormatter(reader);
                Console.WriteLine($"{id}: Deleted.");
                }
            catch(Exception ex)
                {
                Console.WriteLine(ex.Message);
                return null;
                }
            finally
                {
                _sqlData.CloseConnection();
                }
            return person;
            }
        }
    }
    
