using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.models
    {
    internal class MenuUI
        {
        private Controllers controllers;
        private Person reporter = null;
        public MenuUI(Controllers con)
            {
            controllers = con;
            Console.WriteLine("Welcome To the Malshinon System.");
            while (true)
                {
                 while (reporter == null)
                    {
                    LoginFlow();
                    }
                MenuFlow();
                }
           

            }
        private void LoginFlow()
            {
            Console.WriteLine("To log-in enter your Secret Code: ");
            string SecretCode = Console.ReadLine();
            reporter = controllers.LogIn(SecretCode);
            }

        private void MenuFlow()
            {
            Console.WriteLine("Menu: \n" +
                "1. Report Intel.\n" +
                "2. View Previous Reports\n" +
                "3. View Reports by Target\n" +
                "4. Log Out\n" +
                "5. Quit\n" +
                "Enter Your Choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
                {
                case 1:
                    Person target = GetTargetFlow();
                    controllers.CreateReport(reporter, target);
                    break;
                case 2:
                    List<IntelReport> reports = controllers.GetIntelReportsByReporter(reporter);
                    Console.WriteLine("--- Your Reports ---");
                    PrintReports(reports);
                    break;
                case 3:
                    target = GetTargetFlow();
                    List<IntelReport> targetsReports = controllers.GetIntelReportsByTarget(target);
                    PrintReports(targetsReports);
                    break;
                case 4:
                    reporter = null;
                    break;
                case 5:
                    System.Environment.Exit(0);
                    break;


                }
            }

        private Person GetTargetFlow()
            {
            string SecretCode = null;
            while (SecretCode == null)
                {
                Console.WriteLine("Enter your target's Secret Code: ");
                SecretCode = Console.ReadLine();
                }
            Person target = controllers.GetTarget(SecretCode);
            return target;
            }
        private void PrintReports(List<IntelReport> reports)
            {
            if(reports != null)
                {
                 Console.WriteLine("---   Reports   ---");
                    foreach (IntelReport report in reports)
                    {
                    report.Printer();
                    }
                    Console.WriteLine("--------------------");
                }
            else
                {
                Console.WriteLine("No Reports Found.");
                }
            
            }
        }
    }
