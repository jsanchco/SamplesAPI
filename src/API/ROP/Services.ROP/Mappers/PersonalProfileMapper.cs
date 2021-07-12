using Model.ROP.Entities;
using Shared.DTO.ROP;

namespace Services.ROP.Mappers
{
    public static class PersonalProfileMapper
    {
        public static PersonalProfileEntity MapToEntity(this PersonalProfileDto personalProfileDto)
        {
            return new PersonalProfileEntity(
                personalProfileDto.Name,
                personalProfileDto.FirstName,
                personalProfileDto.Email,
                personalProfileDto.Age);
        }

        public static PersonalProfileDto MapToDto(this PersonalProfileEntity personalProfileEntity)
        {
            return new PersonalProfileDto 
            {
                Id = personalProfileEntity.Id,
                Name = personalProfileEntity.Name,
                FirstName = personalProfileEntity.FirstName,
                Email = personalProfileEntity.Email,
                Age = personalProfileEntity.Age,
                TotalInformation = $"[]FuylName: {personalProfileEntity.Name} {personalProfileEntity.FirstName}, Age: {personalProfileEntity.Age}",
                IsAdult = personalProfileEntity.Age >= 18
            };
        }
    }
}
