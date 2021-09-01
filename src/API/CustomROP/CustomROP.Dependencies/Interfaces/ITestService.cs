using CustomROP.Dependencies.Model;
using CustomROP.Dependencies.Model.Common;

namespace CustomROP.Dependencies.Interfaces
{
    public interface ITestService
    {
        Result<Person> Operation1();
        Result<Person> Operation2();
    }
}
