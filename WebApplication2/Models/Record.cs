using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Record
    {
        public bool select;
        public Person user;

        public Record(Person user,bool select = false)
        {
            this.user = user;
            this.select = select;
        }
    }
}
