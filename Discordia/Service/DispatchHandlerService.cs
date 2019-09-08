using Discordia.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discordia.Extensions;
using Discordia.Data.Enum;
using Discordia.Data.EventData;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Discordia.Service
{
    public class DispatchHandlerService
    {
        ILogger<DispatchHandlerService> _logger;
        MessageCreateHandlerService _messageCreateHandlerService;
        UserService _userService;

        public DispatchHandlerService(ILogger logger, MessageCreateHandlerService messageCreateHandlerService, UserService userService)
        {
            _logger = logger as ILogger<DispatchHandlerService>;
            _messageCreateHandlerService = messageCreateHandlerService;
            _userService = userService;
        }

        public async Task HandleAsync(GatewayPayload payload)
        {

            var s = JsonConvert.SerializeObject(payload, Formatting.Indented);

            _logger.LogInformation(s);

            var dispatchEvent = payload.GetEventData();

            if (dispatchEvent is MessageCreateEventData)
            {
                var messageCreateEventData = dispatchEvent as MessageCreateEventData;
                await _messageCreateHandlerService.HandleAsync('!', messageCreateEventData);
            }
            else if (dispatchEvent is ReadyEventData)
            {
                var readyEventData = dispatchEvent as ReadyEventData;
                _userService.DiscordAuthInfo.User = readyEventData.User;
            }
        }
    }
}
