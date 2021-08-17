using Model.ROP.Entities;
using Shared.ROP;
using System;
using System.Collections.Immutable;
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

        public async Task<Result<PersonalProfileEntity>> AddPersonalProfile(PersonalProfileEntity personalProfileEntity)
        {
            if (personalProfileEntity.Name.Equals("error", StringComparison.InvariantCulture))
            {
                var errors = ImmutableArray.Create(Error.Create("Error custom at Insert in DB"));

                return await Task.FromResult(new Result<PersonalProfileEntity>(errors));
            }

            return await Task.FromResult(personalProfileEntity);
        }
    }
}
