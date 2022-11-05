using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace EFInDepth.Presistance.Entities
{
    // [Index(Name = nameof(Id))]
    public class Person
    {
        // private int Age;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PersonType PersonType { get; set; }

        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"{Id}, {FirstName} at {CreatedAt}";
        }
    }
}
