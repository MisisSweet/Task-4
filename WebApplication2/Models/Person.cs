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
        public string ShortDateRegistr { get { return Information.DateRegistr.ToShortDateString(); } }
        public string ShortDateLastLogin { get { return Information.LastLogin.ToShortDateString(); } }


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
}
