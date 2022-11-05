// See https://aka.ms/new-console-template for more information
using EFInDepth.Data.Seeding;
using EFInDepth.Presistance;
using EFInDepth.TrashDemos;
using Microsoft.EntityFrameworkCore.Internal;

Console.WriteLine("Hello, World!");

/// First working with utc time converter 
TimeConverter.GetCurrentTypeInfo();


var _dbcontext = new ApplicationDbContext();

/// insert person and retrive it to validate add configuration 
/// 
var personSeeder = new PersonSeed(_dbcontext);
personSeeder.InsertSomeRecords();
Console.WriteLine();
foreach (var item in _dbcontext.Persons.ToList())
{
    Console.WriteLine(item);
}
Console.ReadLine();