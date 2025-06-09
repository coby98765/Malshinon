using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.models;


namespace Malshinon.models
    {
    internal class Person
        {
        public int ID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string SecretCode { get; private set; }
        public string Type { get; private set; }
        public int NumReports { get; private set; }
        public int NumMentions { get; private set; }

        Person(string fName,string lName,string sCode,string type, int Reports,int Mentions,int id=0)
            {
            ID = id;
            FirstName = fName;
            LastName = lName;
            SecretCode = sCode;
            Type = type;
            NumReports = Reports;
            NumMentions = Mentions;
            }

        public string SetType(string newType)
            {
            string[] options = { "reporter", "target", "both", "potential_agent" };
            if (options.Contains(newType))
                {
                Type = newType;
                return newType;
                }
            else
                {
                return null;
                }
            }

        public void IncReports()
            {
                NumReports++;
            }

        public void IncMentions()
            {
            NumMentions++;
            }

        public void Printer()
            {
            Console.WriteLine($"({ID}){SecretCode},\n" +
                $"{Type}: {FirstName} {LastName},\n" +
                $"NumReports: {NumReports},\n" +
                $"NumMentions: {NumMentions}.\n");
            }
        }
    }
