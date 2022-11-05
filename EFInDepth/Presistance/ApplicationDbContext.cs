using EFInDepth.Presistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFInDepth.Presistance
{
    public class ApplicationDbContext:DbContext
    {
        private readonly string connectionString = "Server=.;Database=EFInDepth;Trusted_Connection=True;";
        public ApplicationDbContext()
        {
            
        }

        public DbSet<Person> Persons { get; set; }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString).
                            LogTo(Console.Write, Microsoft.Extensions.Logging.LogLevel.Information);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //foreach (var entity in Model.GetEntityTypes())
            //{
            //    if( entity is Person)
            //        Entry(entity).Property("UpdatedOn").CurrentValue = DateTime.Now;
            //}

            modelBuilder.Entity<Person>().
                HasKey(c => c.Id);

            
            modelBuilder.Entity<Person>().
                Property(c => c.FirstName).
                HasMaxLength(225);

            modelBuilder.Entity<Person>().
                Property(c => c.LastName).
                HasMaxLength(225).
                IsUnicode(false);

            //var utcConverter = new ValueConverter<DateTime, DateTime>(
            //    toDb => TimeZoneInfo.ConvertTimeFromUtc(toDb, TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time")) , 
            //    fromDb => TimeZoneInfo.ConvertTimeToUtc(fromDb));


            var utcConverter = new ValueConverter<DateTime, DateTime>(
                toDb => TimeZoneInfo.ConvertTimeToUtc(toDb),
                fromDb => TimeZoneInfo.ConvertTimeFromUtc(fromDb, TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time")));

            modelBuilder.Entity<Person>()
                    .Property(e => e.CreatedAt)
                    .HasConversion(utcConverter);

            modelBuilder.Entity<Person>()
            .Property(e => e.PersonType)
            .HasConversion(
                to => to.ToString(),
                from => (PersonType)Enum.Parse(typeof(PersonType), from)
                ).IsUnicode(false);

            //modelBuilder.Entity<Person>().
            //    Property<DateTime>("LastUpdate");


            /// set configuration for global needs 
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var entityProperty in entityType.GetProperties())
                {
                    if (entityProperty.ClrType == typeof(DateTime)
                        && entityProperty.Name.EndsWith("Utc"))
                    {
                        entityProperty.SetValueConverter(utcConverter);
                    }
                    if (entityProperty.ClrType == typeof(decimal) && entityProperty.Name.Contains("Price"))
                    {
                        entityProperty.SetPrecision(9);
                        entityProperty.SetScale(2);
                    }

                    if (entityProperty.ClrType == typeof(string)
                        && entityProperty.Name.EndsWith("Url"))
                    {
                        entityProperty.SetIsUnicode(false);
                    }
                    //... other examples left out for clarity
                }
            }
        

            base.OnModelCreating(modelBuilder);
        }
    }
}
