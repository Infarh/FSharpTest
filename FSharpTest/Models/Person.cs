using System;
using System.Collections.Generic;
using System.Text;

namespace FSharpTest.Models
{
    class Person
    {
        public string FullName { get; }

        public Person(string Surname, string Name) => FullName = $"{Surname} {Name}";


    }
}
