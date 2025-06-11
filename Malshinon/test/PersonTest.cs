using Malshinon.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.models
    {
    internal class PersonTest
        {
        public PersonTest(PersonDAL personDal)
            {
            PersonDAL personDAL = personDal;
            //testing PersonDAL CRUD
            //Create
            Person person = new Person("yaakov", "levi", "TheX909", "target", 0, 0);
            person = personDAL.CreatePerson(person);
            Console.WriteLine("Added Person.");
            //read
            List<Person> people = personDAL.GetPeopleList();
            //update
            people[0].IncMentions();
            people[0].Printer();
            personDAL.UpdatePerson(people[0]);
            personDAL.GetPersonById(people[0].ID).Printer();
            //delete
            //personDAL.DeletePersonById(people[1].ID);
            }
        }
    }
