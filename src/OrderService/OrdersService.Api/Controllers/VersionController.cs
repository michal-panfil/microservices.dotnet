﻿using Microsoft.AspNetCore.Mvc;

namespace OrdersService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return GetAssembyVersion();
        }

        private static string GetAssembyVersion() => typeof(VersionController).Assembly.GetName().Version.ToString();

    }
}
