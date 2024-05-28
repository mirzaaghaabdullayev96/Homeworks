using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace homework_9_Static_Extension
{
    internal class User : IAccount
    {
        private static int counter;
        public string FullName { get; set; }
        public string Email { get; set; }

        private string _password;
        public string Password
        {
            get => _password;
            private set
            {
                _password = value;
            }
        }

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }


        public int Id { get; }

        static User()
        {
            counter = 0;
        }

        public User(string email, string fullname, string password)
        {
            counter++;
            Id = counter;
            FullName = fullname;
            Email = email;
            if (!PasswordChecker(password))
            {
                do
                {
                    Console.WriteLine("Your password must contain AT LEAST 1 digit, 1 upper, 1 lower and must be more than 8 characters.\nEnter new password");
                    password = Console.ReadLine();
                    Password = password;
                }

                while (!PasswordChecker(password));


            }


        }


        public void ShowInfo()
        {
            Console.WriteLine($"Id - {Id}, Fullname - {FullName}, Email - {Email}");
        }

        public bool PasswordChecker(string password)
        {
            if (password.Any(char.IsUpper) && password.Length >= 8 && password.Any(char.IsLower) && password.Any(char.IsDigit))
                return true;

            return false;
        }

        #region creating user by method

        //static password checker for static creating new user method
        //public static bool PasswordChecker(string password)
        //{
        //    if (password.Any(char.IsUpper) && password.Length >= 8 && password.Any(char.IsLower) && password.Any(char.IsDigit))
        //        return true;

        //    return false;
        //}

        ////creating user by static method
        //public static User CreatingNewUser()
        //{
        //    Console.WriteLine("Please, enter your name");
        //    string name = Console.ReadLine();
        //    Console.WriteLine("Please, enter your email");
        //    string email = Console.ReadLine();
        //    Console.WriteLine("Please, enter your password");
        //    string pass = Console.ReadLine();

        //    if (PasswordChecker(pass))
        //    {
        //        Console.WriteLine("New user created");
        //        return new User(email, name, pass);
        //    }

        //    else
        //    {
        //        do
        //        {
        //            Console.WriteLine("Please, enter correct password");
        //            pass = Console.ReadLine();
        //        }
        //        while (!PasswordChecker(pass));

        //        Console.WriteLine("New user created");
        //        return new User(email, name, pass);
        //    }
        //}
        #endregion


    }
}
