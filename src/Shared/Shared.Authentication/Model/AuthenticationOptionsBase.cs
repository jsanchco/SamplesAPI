using Microsoft.AspNetCore.Authentication;
using System;

namespace Shared.Authentication.Model
{
    public class AuthenticationOptionsBase : AuthenticationSchemeOptions
    {
        public virtual string Schema => "Base";

        public TimeSpan AllowedDateDrift { get; set; } = TimeSpan.FromMinutes(5);

        public Func<string, string[]> GetRolesForId { get; set; }
    }
}
