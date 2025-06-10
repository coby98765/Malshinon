using Malshinon.DB;
using Malshinon.models;


namespace Malshinon
    {
    class Program
        {
        static void Main()
            {
            MySqlData sqlData = new MySqlData();
            sqlData.Setup();
            PersonDAL personDAL = new PersonDAL(sqlData);
            IntelReportDAL reportDAL = new IntelReportDAL(sqlData);


            //testing PersonDAL CRUD
            //Create
            //Person person = new Person("yaakov", "levi", "TheMan239", "target",0,0);
            //personDAL.CreatePerson(person);
            //read
            //List<Person> people = personDAL.GetPeopleList();
            //people[0].Printer();
            //update
            //people[0].IncMentions();
            //people[0].Printer();
            //personDAL.UpdatePerson(people[0]);
            //personDAL.GetPersonById(people[0].ID).Printer();
            //delete
            //personDAL.DeletePersonById(people[1].ID);

            //testing IntelReportDAL CRUD
            //Create
            //Person person = new Person("yaakov", "levi", "TheMan239", "target",0,0);
            //personDAL.CreatePerson(person);
            //read
            //List<Person> people = personDAL.GetPeopleList();
            //people[0].Printer();
            //update
            //people[0].IncMentions();
            //people[0].Printer();
            //personDAL.UpdatePerson(people[0]);
            //personDAL.GetPersonById(people[0].ID).Printer();
            //delete
            //personDAL.DeletePersonById(people[1].ID);
            }
        }
    }