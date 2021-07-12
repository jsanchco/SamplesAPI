using Model.ROP.Entities;
using System;
using System.Threading.Tasks;

namespace Data.ROP.Repositories
{
    public class PersonalProfileRepository
    {
        public async Task<PersonalProfileEntity> GetPersonalProfile(Guid id)
        {
            return await Task.FromResult(new PersonalProfileEntity 
            (
                "Jesús", 
                "Sánchez", 
                "jsanchco@gmail.com",
                48
            ));
        }

        public async Task<bool> AddPersonalProfile(PersonalProfileEntity personalProfileEntity)
        {
            return await Task.FromResult(true);
        }
    }
}
