using FunWithReflextion.Models;
using System;
using System.Data;

namespace FunWithReflextion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataTable usersTable = User.All();
            DataTable peopleTable = People.All();

            Console.WriteLine("Users:");
            foreach (DataRow row in usersTable.Rows)
            {
                int id = (int)row["Id"];
                string name = (string)row["Name"];
                string email = (string)row["Email"];

                Console.WriteLine($"ID: {id}, Name: {name}, Email: {email}");
            }

            Console.WriteLine("\nPeople:");
            foreach (DataRow row in peopleTable.Rows)
            {
                int id = (int)row["Id"];
                string name = (string)row["Name"];
                string email = (string)row["Email"];
                string phone = (string)row["Phone"];
                Console.WriteLine($"ID: {id}, Name: {name}, Email: {email}, Phone: {phone}");
            }
        }
    }
}
