using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {

        private readonly IConfiguration configuration;

        public LocalMailService(IConfiguration config)
        {
            configuration = config;
        }
        public void SendEmail(string subject, string message)
        {

            string to = $"{configuration["mailSettings:mailTo"]} and {configuration["mailSettings:mailFrom"]}";

            // does stuff
        }
    }
}
