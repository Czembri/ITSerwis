using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUpdater
{
    public  class ProgramLogic : IProgramLogic
    {
        UserModule _userModule = new UserModule();
        public void ProgramLogicMethod(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Enter the arguments!");
            }
            else
            {

                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-f":
                            Console.WriteLine($"Firstname : '{args[i + 1]}'");
                            _userModule.FirstName = args[i + 1];
                            break;
                        case "-l":
                            Console.WriteLine($"Lastname : '{args[i + 1]}'");
                            _userModule.LastName = args[i + 1];
                            break;
                        case "-u":
                            Console.WriteLine($"Username : '{args[i + 1]}'");
                            _userModule.Username = args[i + 1];
                            break;
                        case "-p":
                            Console.WriteLine($"Password : '{args[i + 1]}'");
                            _userModule.Password = args[i + 1];
                            break;
                        case "-a":
                            Console.WriteLine($"Age : '{args[i + 1]}'");
                            _userModule.Age = args[i + 1];
                            break;
                    }
                }

                _userModule.InsertUser();

            }
        }
    }
}
