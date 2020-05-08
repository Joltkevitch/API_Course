using CityInfo.API.Contexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/testdatabase")]
    public class TestController : ControllerBase
    {
        private readonly CityInfoContext _context;

        public TestController(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public IActionResult getTest()
        {
            return Ok();
        }
    }
}
