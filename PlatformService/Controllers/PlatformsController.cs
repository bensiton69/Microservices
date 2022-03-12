using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.DTOs;
using PlatformService.Interfaces;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public async Task<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            return await _unitOfWork.PlatformRepository.GetAllPlatforms();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlatformReadDto>> GetPlatform(int id)
        {
            var platform = await _unitOfWork.PlatformRepository.GetPlatform(id);

            if (platform == null)
            {
                return NotFound();
            }

            return platform;
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> PostPlatform([FromBody] PlatformCreateDto platformCreateDto)
        {
            PlatformReadDto platformReadDto = _unitOfWork.PlatformRepository.Add(platformCreateDto);
            await _unitOfWork.CompleteAsync();

            //Send sync msg
            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            //Send Async msg
            try
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);
                platformPublishedDto.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return Ok(platformReadDto);
        }
    }
}
