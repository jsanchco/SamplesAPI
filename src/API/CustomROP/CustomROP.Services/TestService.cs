using CustomROP.Dependencies.Interfaces;
using CustomROP.Dependencies.Model;
using CustomROP.Dependencies.Model.Common;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CustomROP.Services
{
    public class TestService : ITestService
    {
        public Result<Person> Operation1()
        {
            var person = new Person
            {
                Name = "Jesús"
            };

            return person;
        }

        public Result<Person> Operation2()
        {
            var person = new Person
            {
                Name = "Jesús"
            };

            var errors = new List<Error>
            {
                Error.Create("Ha ocurrido un error")
            };

            return errors.Any()
                  ? Result.Failure<Person>(errors.ToImmutableArray())
                  : person;
        }
    }
}
