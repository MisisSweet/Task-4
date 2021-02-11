using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Person
    {
        public static Person LoginUser;
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        
        public Information Information { get; set; }
        public string StringStatus { get
            {
                return Information.Status == 1 ? "Заблокирован" : "Разблокирован";
            } }
        public string FullName
        {
            get
            {
                return Information.FirstName+" "+Information.LastName;
            }
        }
        public Person() { }
        public Person(int iD, string login, string password, Information information)
        {
            ID = iD;
            Login = login;
            Password = password;
            Information = information;
        }
    }

    public class Information
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateRegistr { get; set; }
        public DateTime LastLogin { get; set; }
        public int Status { get; set; }

        public Information(int iD, string firstName, string lastName, string email, DateTime dateRegistr, DateTime lastLogin, int status)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateRegistr = dateRegistr;
            LastLogin = lastLogin;
            Status = status;
        }
    }
}
