namespace FSharpTest.Models
{
    internal class Person
    {
        public string Surname { get; }
        public string Name { get; }
        public int Age { get; }
        public Address Address { get; }
        public string FullName => $"{Surname} {Name}";

        public Person(string Surname, string Name, int Age = 0, Address Address = null)
        {
            this.Surname = Surname;
            this.Name = Name;
            this.Age = Age;
            this.Address = Address;
        }

        public Person WhereSurname(string Surname) => new Person(Surname, Name, Age, Address);
        public Person WhereName(string Name) => new Person(Surname, Name, Age, Address);
        public Person WhereAge(int Age) => new Person(Surname, Name, Age, Address);
        public Person WhereAddress(Address Address) => new Person(Surname, Name, Age, Address);
    }

    internal class Address
    {
        public string ZipCode { get; }
        public string City { get; }
        public string Street { get; }

        public Address(string ZipCode, string City, string Street)
        {
            this.ZipCode = ZipCode;
            this.City = City;
            this.Street = Street;
        }

        public Address WhereZipCode(string ZipCode) => new Address(ZipCode, City, Street);
        public Address WhereCity(string City) => new Address(ZipCode, City, Street);
        public Address WhereStreet(string Street) => new Address(ZipCode, City, Street);
    }
}
