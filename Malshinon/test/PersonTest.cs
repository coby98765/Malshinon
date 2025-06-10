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
            Person person = new Person("yaakov", "levi", "TheMan239", "target", 0, 0);
            personDAL.CreatePerson(person);
            //read
            List<Person> people = personDAL.GetPeopleList();
            people[0].Printer();
            //update
            people[0].IncMentions();
            people[0].Printer();
            personDAL.UpdatePerson(people[0]);
            personDAL.GetPersonById(people[0].ID).Printer();
            //delete
            personDAL.DeletePersonById(people[1].ID);
            }
        }
    }
