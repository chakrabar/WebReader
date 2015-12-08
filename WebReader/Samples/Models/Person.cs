using System;
using System.Collections.Generic;

namespace WebReader.Samples.Models
{
    public class Person
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Address Address { get; set; }

        public static IEnumerable<Person> GetPersons()
        {
            var p1 = new Person
            {
                Name = "Hulk",
                BirthDate = DateTime.Now.AddYears(-30).AddDays(-72),
                Address = new Address
                {
                    AddressLine1 = "DC lane",
                    Country = "Comics"
                }
            };
            var p2 = new Person
            {
                Name = "SuperMan",
                BirthDate = DateTime.Now.AddYears(-27).AddDays(52),
                Address = new Address
                {
                    AddressLine1 = "Daily Planet",
                    Country = "Comics"
                }
            };
            var p3 = new Person
            {
                Name = "BatMan",
                BirthDate = DateTime.Now.AddYears(-31).AddDays(-7),
                Address = new Address
                {
                    AddressLine1 = "Wayne House",
                    City = "Gotham",
                    Country = "Comics"
                }
            };
            return new List<Person> { p1, p2, p3 };
        }
    }
}