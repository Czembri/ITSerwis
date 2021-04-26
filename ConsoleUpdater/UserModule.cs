using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleUpdater.Models;
using ItSerwis.Model;
using ItSerwis_Merge_v2;
using Microsoft.EntityFrameworkCore;

namespace ConsoleUpdater
{
    public class UserModule : Encryptor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Age { get; set; } = "9999";

        public void InsertUser()
        {
            string encrLogin = EncryptData(Username);
            string encrPass = EncryptData(Password);

            using (var db = new DbContextClass())
            {
                var checkIfDataInDb = db.UserLogin.FirstOrDefault(e => e.LoginHash == encrLogin);
                if (checkIfDataInDb == null)
                {
                    var userLogin = new UserLogin
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        Age = Int32.Parse(Age),
                        LoginHash = encrLogin,
                        PasswordHash = encrPass
                    };

                    try
                    {
                        db.UserLogin.Add(userLogin);
                        Console.WriteLine("Inserting data to USERLOGIN table...");
                        db.SaveChanges();

                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err);
                    }

                }else
                {
                    Console.WriteLine("User already exists");
                    Environment.Exit(0);
                }

            }
        }
    }


}
