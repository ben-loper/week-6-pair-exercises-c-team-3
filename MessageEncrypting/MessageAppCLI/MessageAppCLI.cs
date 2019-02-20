using System;
using System.Collections.Generic;
using System.Text;

namespace MessageAppCLI
{
    public class MessageAppCLI
    {        

        public void Run()
        {
            InitialScreen();
        }

        private void InitialScreen()
        {
            bool quit = false;
            while (!quit)
            {
                Console.Clear();
                Console.WriteLine("Welcome\n1. Log in\n2. Create a New User\nQ. Quit");
                string userChoice = Console.ReadLine();

                if (userChoice == "1")
                {
                    LoginMenu();
                }
                else if (userChoice == "2")
                {
                    CreateUserMenu();
                }
                else if (userChoice.ToLower() == "q")
                {
                    quit = true;
                }
            }
        }

        private void LoginMenu()
        {
            Console.Clear();
            Console.Write("Hi");
            Console.ReadKey();
        }

        private void CreateUserMenu()
        {

        }
    }
}
