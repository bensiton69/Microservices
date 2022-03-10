using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Interfaces
{
    public interface IPlatformRepository
    {
        Task<IEnumerable<PlatformReadDto>> GetAllPlatforms();
        Task<PlatformReadDto> GetPlatform(int id);
        PlatformReadDto Add(PlatformCreateDto platformCreate);
    }
}
