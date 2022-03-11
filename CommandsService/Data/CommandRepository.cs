using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Interfaces;
using CommandsService.Models;

namespace CommandsService.Data
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CommandRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CommandReadDto CreateCommand(int platformId, CommandCreateDto commandCreateDto)
        {
            var command = _mapper.Map<Command>(commandCreateDto);

            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;
            _context.Commands.Add(command);

            return _mapper.Map<CommandReadDto>(command);
        }

        public void CreatePlatform(Platform plat)
        {
            if (plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }
            _context.Platforms.Add(plat);
        }

        public bool ExternalPlatformExists(int externalPlatformId)
        {
            return _context.Platforms.Any(p => p.ExternalID == externalPlatformId);
        }

        public IEnumerable<PlatformreadDto> GetAllPlatforms()
        {
            return _mapper.Map<IEnumerable<PlatformreadDto>>(_context.Platforms.ToList());
        }

        public CommandReadDto GetCommand(int platformId, int commandId)
        {
            var command = _context.Commands
                .Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();

            return _mapper.Map<CommandReadDto>(command);
        }

        public IEnumerable<CommandReadDto> GetCommandsForPlatform(int platformId)
        {
            var commands = _context.Commands
                .Where(c => c.PlatformId == platformId)
                .OrderBy(c => c.Platform.Name);

            return _mapper.Map<IEnumerable<CommandReadDto>>(commands);
        }

        public bool PlatformExits(int platformId)
        {
            return _context.Platforms.Any(p => p.Id == platformId);
        }
    }
}
