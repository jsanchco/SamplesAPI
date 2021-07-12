using System;

namespace Model.ROP.Entities
{
    public class PersonalProfileEntity
    {
        public readonly Guid Id;
        public readonly string Name;
        public readonly string FirstName;
        public readonly string Email;
        public readonly int Age;

        public PersonalProfileEntity(
            string name, 
            string firstName, 
            string email,
            int age)
        {
            Id = Guid.NewGuid();

            Name = name;
            FirstName = firstName;
            Email = email;
            Age = age;
        }
    }
}
