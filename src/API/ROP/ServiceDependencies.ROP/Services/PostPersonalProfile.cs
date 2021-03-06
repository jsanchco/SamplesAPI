using Data.ROP.Repositories;
using Microsoft.Extensions.Logging;
using Model.ROP.Entities;
using Services.ROP.Interfaces;
using Services.ROP.Mappers;
using Shared.DTO.ROP;
using Shared.ROP;
using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace ServiceDependencies.ROP.Services
{
    public class PostPersonalProfile : IPostPersonalProfile
    {
        private readonly ILogger<PostPersonalProfile> _logger;
        private readonly PersonalProfileRepository _personalProfileRepository;
        private readonly Random _random;

        public PostPersonalProfile(
            ILogger<PostPersonalProfile> logger,
            PersonalProfileRepository personalProfileRepository)
        {
            _logger = logger;
            _personalProfileRepository = personalProfileRepository;

            _random = new Random();
        }

        public async Task<Result<PersonalProfileEntity>> AddPersonalProfile(PersonalProfileDto personalProfileDto)
        {
            var result = await _personalProfileRepository
                .AddPersonalProfile(personalProfileDto.MapToEntity());

            return result;
        }

        public async Task<Result<bool>> AddPersonalProfileAndSendEmail(PersonalProfileDto personalProfileDto)
        {
            var result = await _personalProfileRepository
                .AddPersonalProfile(personalProfileDto.MapToEntity())
                .Bind(x => SendEmail(personalProfileDto));

            return result;
        }

        private async Task<Result<bool>> SendEmail(PersonalProfileDto personalProfileDto)
        {
            var rnd = _random.Next(0, 2);
            if (rnd == 0)
            {
                // Simulamos un error
                var messageError = $"Fail in Send email [{personalProfileDto.Email}]";
                _logger.LogError(messageError);
                var errors = ImmutableArray.Create(Error.Create(messageError));
                return await Task.FromResult(errors);
            }

            _logger.LogError($"Send email [{personalProfileDto.Email}] to {personalProfileDto.TotalInformation}");
            return await Task.FromResult(true);
        }
    }
}
