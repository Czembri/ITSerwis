using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace ConsoleUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Enter the arguments!");
            } else
            {

                var us = new UserModule();

                us.Username = args[0];
                us.Password = args[1];
                us.Age = args[2];

                us.InsertNewUserCommand();

            }
        }
    }
}
