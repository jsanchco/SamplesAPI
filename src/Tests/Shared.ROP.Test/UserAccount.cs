namespace Shared.ROP.Test
{
    public class UserAccount
    {
        public readonly string UserName;
        public readonly string FirstName;
        public readonly string LastName;
        public readonly string Email;

        public UserAccount(string userName, string firstName, string lastName, string email)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public override string ToString()
        {
            return $"FullName: {UserName} {FirstName} {LastName}, Email: {Email}";
        }
    }
}
