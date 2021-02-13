using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{

    public class SelectablePerson : Person
    {
        public bool IsSelected { get; set; }
    }
}
