namespace FSharpTest.Models
{
    internal class Person
    {
        public string Surname { get; }
        public string Name { get; }
        public int Age { get; }
        public string FullName => $"{Surname} {Name}";
        public Address Address { get; }

        public Person(string Surname, string Name, int Age = 0, Address Address = null)
        {
            this.Surname = Surname;
            this.Name = Name;
            this.Age = Age;
            this.Address = Address;
        }
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
    }
}
