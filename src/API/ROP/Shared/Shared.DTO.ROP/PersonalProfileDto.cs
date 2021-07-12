using System;

namespace Shared.DTO.ROP
{
    public class PersonalProfileDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string TotalInformation { get; set; }
        public bool IsAdult { get; set; }
    }
}
