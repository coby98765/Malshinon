using Malshinon.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.models
    {
    internal class Controllers
        {
        private PersonDAL personDAL;
        private IntelReportDAL intelReportDAL;

        public Controllers(PersonDAL _personDAL, IntelReportDAL _intelReportDAL)
            {
            personDAL = _personDAL;
            intelReportDAL = _intelReportDAL;
            }
        //create person
        public Person? CreatePerson(string secret, string type)
            {
            string? firstName = null;
            string? lastName = null;

            while (firstName == null)
                {
                Console.WriteLine("Enter First Name: ");
                firstName = Console.ReadLine();
                }
            while (lastName == null)
                {
                Console.WriteLine("Enter Last Name: ");
                lastName = Console.ReadLine();
                }

            Person person = new Person(firstName, lastName, secret, type);
            person = personDAL.CreatePerson(person);
            if(person == null)
                {
                Console.WriteLine("Error Creating User.");
                return null;
                }
            Console.WriteLine("User created.");
            return person;
            }



        public Person? LogIn(string secret)
            {
            Person? person = personDAL.GetPersonBySecret(secret);
            if(person == null)
                {
                Console.WriteLine("User not found! Lets Create a new User.");
                person = CreatePerson(secret, "reporter");
                }
            Console.WriteLine("Logged in.");
            person.Printer();
            return person;
            }

        public Person? GetTarget(string secret)
            {
            Person? person = personDAL.GetPersonBySecret(secret);
            if (person == null)
                {
                Console.WriteLine("Target not found! Lets Create a new User.");
                person = CreatePerson(secret, "target");
                }
            Console.WriteLine("Ready to report on Target.");
            person.Printer();
            return person;
            }

        public void CreateReport(Person reporter, Person target)
            {
            string data = null;
            while(data == null)
                {
                Console.WriteLine("Enter Your report:");
                data = Console.ReadLine();
                }

            IntelReport newReport = new IntelReport(reporter.ID, target.ID, data);
            IntelReport response = intelReportDAL.CreateReport(newReport);
            if(response != null)
                {
                Console.WriteLine("Report Created.");
                PostReportActions(reporter, target);
                }
            else
                {
                Console.WriteLine("Error.");
                }
            }

        private bool PostReportActions(Person reporter, Person target)
            {
            try
                {
                reporter.IncReports();
                if(reporter.Type == "reporter" && reporter.NumReports >= 10)
                    {
                    reporter.SetType(CheckTypePromotion(reporter));
                    }
                target.IncMentions();

                //update both in DB
                personDAL.UpdatePerson(reporter);
                personDAL.UpdatePerson(target);
                return true;
                }
            catch(Exception ex)
                {
                Console.WriteLine(ex);
                return false;
                }
            }

        private string CheckTypePromotion(Person reporter)
            {
            if (AverageMsgLen(reporter.ID) > 100)
                {
                Console.WriteLine("You Are up for a potential promotion.");
                return "potential_agent";
                }
            else
                {
                return "reporter";
                }
            }

        private int AverageMsgLen(int reporterID)
            {
            List<IntelReport> allIntelReports = intelReportDAL.GetReportsByReporter(reporterID);
            int reportAmount = allIntelReports.Count();
            int sumOfMsgLen = 0;
            foreach(IntelReport report in allIntelReports)
                {
                sumOfMsgLen += report.Text.Count();
                }
            return sumOfMsgLen / reportAmount;
            }
        }
    }
