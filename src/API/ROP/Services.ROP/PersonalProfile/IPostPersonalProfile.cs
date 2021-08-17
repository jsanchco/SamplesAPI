using Model.ROP.Entities;
using Shared.DTO.ROP;
using Shared.ROP;
using System.Threading.Tasks;

namespace Services.ROP.Interfaces
{
    public interface IPostPersonalProfile
    {
        Task<Result<bool>> AddPersonalProfileAndSendEmail(PersonalProfileDto personalProfileDto);
        Task<Result<PersonalProfileEntity>> AddPersonalProfile(PersonalProfileDto personalProfileDto);
    }
}
