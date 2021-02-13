using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Information
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateRegistr { get; set; }
        public DateTime LastLogin { get; set; }
        public int Status { get; set; }

        public Information() { }
        public Information(int iD, string firstName, string lastName, string email, DateTime dateRegistr, string lastLogin, int status)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateRegistr = dateRegistr;

            DateTime dateTime;
            DateTime.TryParse(lastLogin, out dateTime);
            LastLogin = dateTime;
            Status = status;
        }
    }
}
