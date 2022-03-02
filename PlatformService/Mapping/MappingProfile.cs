using AutoMapper;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            // source -> target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
        }
    }
}
