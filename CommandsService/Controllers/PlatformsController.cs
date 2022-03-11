using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandsService.Dtos;
using CommandsService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlatformsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformreadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms from CommandsService");

            return Ok(_unitOfWork.CommandRepository.GetAllPlatforms());
        }

        [HttpPost]
        public ActionResult Test()
        {
            Console.WriteLine("--> Inbound POST # Command Service");

            return Ok("Inbound test of from Platforms Controler");
        }
    }
}