using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    public class PlatformController : ControllerBase
    {
        public PlatformController()
        {
            
        }

        [HttpPost]
        public ActionResult Test()
        {
            Console.WriteLine("--> Test from commnad service");
            return Ok();
        }
    }
}