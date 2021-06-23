using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.ROP.Extensions;
using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace Shared.ROP.Test
{
    [TestClass]
    public class ROPTest
    {
        private readonly UserAccount _userAccount = new UserAccount("Jesús", "Sánchez", "Corzo", "jsanchco@gmail.com");
        private readonly ImmutableArray<Error> _errors = ImmutableArray.Create(Error.Create("Error 1"), Error.Create("Error 2"));

        [TestInitialize()]
        public void Initialize()
        {
        }

        [TestMethod, TestCategory("Cast")]
        public void Cast_T_Value_ResultOK()
        {
            Trace.WriteLine($"Init: {DateTime.Now:HH:mm:ss}");
            Result<UserAccount> result = _userAccount;
            
            Assert.IsNotNull(result, $"Result is null, but UserAccount is {_userAccount}");
            Trace.WriteLine(message: $"Success: {result.Success}, Value: {result.Value}, {result.Errors.Select(e => e.Message).Prepend($"There is/are {result.Errors.Length} errors:").JoinStrings(Environment.NewLine)}");
            Trace.WriteLine($"End: {DateTime.Now:HH:mm:ss}");
        }

        [TestMethod, TestCategory("Cast")]
        public void Cast_T_Value_WithErrors_ResultOK()
        {
            Trace.WriteLine($"Init: {DateTime.Now:HH:mm:ss}");
            Result<UserAccount> result = _errors;

            Assert.IsNotNull(result, $"Result is null, but errors is {_errors}");
            Trace.WriteLine(message: $"Success: {result.Success}, Value: {result.Value}, {result.Errors.Select(e => e.Message).Prepend($"There is/are {result.Errors.Length} errors:").JoinStrings(Environment.NewLine)}");
            Trace.WriteLine($"End: {DateTime.Now:HH:mm:ss}");
        }
    }
}
