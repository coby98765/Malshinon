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
            LoginFlow();
            }
        private void LoginFlow()
            {
            Console.WriteLine("To log-in enter your Secret Code: ");
            string SecretCode = Console.ReadLine();
            reporter = controllers.LogIn(SecretCode);
            }

        }
    }
