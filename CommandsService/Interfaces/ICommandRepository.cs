using System.Collections.Generic;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.Interfaces
{
    public interface ICommandRepository
    {
        bool SaveChanges();

        // Platforms
        IEnumerable<PlatformreadDto> GetAllPlatforms();
        void CreatePlatform(Platform plat);
        bool PlatformExits(int platformId);
        bool ExternalPlatformExists(int externalPlatformId);

        // Commands
        IEnumerable<CommandReadDto> GetCommandsForPlatform(int platformId);
        CommandReadDto GetCommand(int platformId, int commandId);
        CommandReadDto CreateCommand(int platformId, CommandCreateDto command);
    }
}