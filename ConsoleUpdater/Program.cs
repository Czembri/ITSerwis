using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace ConsoleUpdater
{
    public static class Program
    { 
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Enter the arguments!");
            } else
            {

                var us = new UserModule();
                for (int i=0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-u":
                            us.Username = args[i+1];
                            break;
                        case "-p":
                            us.Password = args[i + 1];
                            break;
                        case "-a":
                            us.Age = args[i + 1];
                            break;
                        default:
                            Console.WriteLine("Default");
                            break;
                    }
                }

                us.InsertNewUserCommand();

            }
        }
    }

}
