using System;
using System.Collections.Generic;
using CommandsService.Dtos;
using CommandsService.Interfaces;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommandsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformId}");

            if (!_unitOfWork.CommandRepository.PlatformExits(platformId))
            {
                return NotFound();
            }

            return Ok(_unitOfWork.CommandRepository.GetCommandsForPlatform(platformId));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId} / {commandId}");

            if (!_unitOfWork.CommandRepository.PlatformExits(platformId))
            {
                return NotFound();
            }

            var command = _unitOfWork.CommandRepository.GetCommand(platformId, commandId);

            if (command == null)
            {
                return NotFound();
            }

            return Ok(command);
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformId}");

            if (!_unitOfWork.CommandRepository.PlatformExits(platformId))
            {
                return NotFound();
            }
            CommandReadDto  commandReadDto = _unitOfWork.CommandRepository.CreateCommand(platformId, commandDto);
            _unitOfWork.CompleteAsync();

            return CreatedAtRoute(nameof(GetCommandForPlatform),
                new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);
        }
    }
}
