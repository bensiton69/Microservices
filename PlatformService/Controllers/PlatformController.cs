using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlatformService.DTOs;
using PlatformService.Interfaces;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    public class PlatformController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlatformController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        public async Task<ActionResult<PlatformReadDto>> PostPlatform([FromBody]PlatformCreateDto platformCreateDto)
        {
            PlatformReadDto platformReadDto = await _unitOfWork.PlatformRepository.Add(platformCreateDto);
            await _unitOfWork.CompleteAsync();
            return Ok(platformReadDto);
        }
    }
}
