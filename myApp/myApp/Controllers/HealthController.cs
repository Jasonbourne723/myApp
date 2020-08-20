using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace myApp.Controllers
{
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        static int i = 0;
        public IActionResult Get()
        {
            if (i == 5)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}