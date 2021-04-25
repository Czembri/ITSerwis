using System;
using System.Collections.Generic;

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
                        case "-f":
                            Console.WriteLine($"Firstname : '{args[i + 1]}'");
                            us.FirstName = args[i + 1];
                            break;
                        case "-l":
                            Console.WriteLine($"Lastname : '{args[i + 1]}'");
                            us.LastName = args[i + 1];
                            break;
                        case "-u":
                            Console.WriteLine($"Username : '{args[i + 1]}'");
                            us.Username = args[i+1];
                            break;
                        case "-p":
                            Console.WriteLine($"Password : '{args[i + 1]}'");
                            us.Password = args[i + 1];
                            break;
                        case "-a":
                            Console.WriteLine($"Age : '{args[i + 1]}'");
                            us.Age = args[i + 1];
                            break;
                    }
                }

                us.InsertUser();

            }
        }
    }

}
