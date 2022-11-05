using EFInDepth.Presistance;
using EFInDepth.Presistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFInDepth.Data.Seeding
{
    public class PersonSeed
    {
        private readonly ApplicationDbContext _dbContext;
        public PersonSeed(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InsertSomeRecords()
        {
            var person1 = new Person()
            {
                FirstName = "Faten",
                LastName = "Elhariry",
                PersonType = PersonType.Normal,
                CreatedAt = DateTime.Now
            };
            _dbContext.Add<Person>(person1);
            _dbContext.SaveChanges();
        }
    }
}
