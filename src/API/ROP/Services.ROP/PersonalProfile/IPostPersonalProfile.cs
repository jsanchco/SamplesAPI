using Shared.DTO.ROP;
using Shared.ROP;
using System.Threading.Tasks;

namespace Services.ROP.Interfaces
{
    public interface IPostPersonalProfile
    {
        Task<Result<PersonalProfileDto>> AddPersonalProfileAndSendEmail(PersonalProfileDto personalProfileDto);
        Task<Result<PersonalProfileDto>> AddPersonalProfile(PersonalProfileDto personalProfileDto);
    }
}
