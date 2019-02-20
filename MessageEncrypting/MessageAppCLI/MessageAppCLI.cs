using System;
using MessageEncrypting;
using System.Collections.Generic;
using System.Text;
using MessageEncrypting.BusinessLogic;

namespace MessageAppCLI
{
    public class MessageAppCLI
    {
        private MessageApp _ma;

        public MessageAppCLI(MessageApp ma)
        {
            _ma = ma;
        }


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
            Console.WriteLine("Enter username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();
            try
            {
                _ma.LoginUser(username, password);
                //Console.WriteLine($"Welcome {_vm.CurrentUser.FirstName} {_vm.CurrentUser.LastName}");
               // Console.ReadKey();
               //VendingMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        private void CreateUserMenu()
        {
            bool isMatch = false;
            while (!isMatch)
            {
                Console.Clear();
                Console.WriteLine("Enter username: ");
                string username = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                string password = Console.ReadLine();
                Console.WriteLine("Confirm your password: ");
                string confirmPassword = Console.ReadLine();

                if (password == confirmPassword)
                {
                    User user = new User();
                    user.Password = password;
                    user.UserName = username;
                    _ma.RegisterUser(user);
                    isMatch = true;
                }
                else
                {
                    Console.WriteLine("Your passwords did not match please enter again.");
                    Console.ReadKey();
                }
            }


        }
    }
}
