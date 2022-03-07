using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {
            
        }

        [HttpPost]
        public ActionResult Test()
        {
            Console.WriteLine("--> Inbound POST # Command Service");

            return Ok("Inbound test of from Platforms Controler");
        }
    }
}