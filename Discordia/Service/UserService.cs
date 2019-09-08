using Discordia.Data.User;
using Discordia.Network;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discordia.Service
{
    public class UserService
    {
        ILogger<UserService> _logger;
        public DiscordAuthInfo DiscordAuthInfo { get; set; }

        public UserService(ILogger<UserService> logger)
        {
            DiscordAuthInfo = new DiscordAuthInfo();
            _logger = logger;
        }
    }
}
