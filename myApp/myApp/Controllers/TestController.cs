using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRedis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace myApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("[action]")]
        public string Set()
        {
            var rng = new Random();

            IRedisClient redis = new RedisClient("redisDb", 6379);
            redis.Set("test", "mywifi");
            return "OK";
        }

        [HttpGet("[action]")]
        public string Get()
        {
            var rng = new Random();

            IRedisClient redis = new RedisClient("redisDb", 6379);
            var value =  redis.Get("test");
            value += "a";
            return "value";
        }
    }
}
