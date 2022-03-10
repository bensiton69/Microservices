using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlatformService.DTOs;
using PlatformService.Interfaces;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PlatformRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            var platforms = await _context.Platforms
                .ToListAsync();
            return _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
        }

        public async Task<PlatformReadDto> GetPlatform(int id)
        {
            return _mapper.Map<Platform, PlatformReadDto>(await _context.Platforms
                .FirstOrDefaultAsync(p => p.Id == id)) ;
        }

        public PlatformReadDto Add(PlatformCreateDto platformCreateDto)
        {
            Platform platform = _mapper.Map<PlatformCreateDto, Platform>(platformCreateDto);
            _context.Platforms.Add(platform);
            return _mapper.Map<Platform, PlatformReadDto>(platform);

        }
    }
}
