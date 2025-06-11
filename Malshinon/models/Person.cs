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

        public Person(string fName,string lName,string sCode,string type, int Reports=0,int Mentions=0,int id=0)
            {
            ID = id;
            FirstName = fName;
            LastName = lName;
            SecretCode = sCode;
            Type = SetType(type);
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
            CheckBothType();
            }

        public void IncMentions()
            {
            NumMentions++;
            if(NumMentions >= 20) { DangerAlert(); }
            CheckBothType();
            }
        private void DangerAlert()
            {
            string FullMsg = $"###   ! DANGER !   ###";
            string border = new string('#', FullMsg.Length);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"{border}\n{border}\n{FullMsg}\n{border}\n{border}\n");
            Printer();
            Console.ResetColor();
            }

        private void CheckBothType()
            {
            if(NumReports>0 && NumMentions > 0)
                {
                SetType("both");
                }
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
