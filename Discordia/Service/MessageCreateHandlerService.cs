using Discordia.Data;
using Discordia.Data.EventData;
using Discordia.Data.Rest;
using Discordia.Network;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Discordia.Service
{
    public class MessageCreateHandlerService
    {
        ILogger<MessageCreateHandlerService> _logger;
        UserService _userService;

        public MessageCreateHandlerService(ILogger logger, UserService userService)
        {
            _logger = logger as ILogger<MessageCreateHandlerService>;
            _userService = userService;
        }

        public async Task HandleAsync(char expectedPrefix, MessageCreateEventData eventData)
        {
            var currPrefix = eventData.Content[0];
            _logger.LogInformation($"{eventData.Author.Username}: {eventData.Content}");

            if (currPrefix == expectedPrefix && eventData.Author.Id != _userService.DiscordAuthInfo.User.Id)
            {
                await HandleCommandAsync(eventData);
            }
        }

        public async Task HandleCommandAsync(MessageCreateEventData eventData)
        {
            string origCommand = eventData.Content.Split(' ')[0];
            string command = origCommand.Substring(1).ToUpper();

            if (command == "ECHO")
            {
                var channelId = eventData.ChannelId;

                // Break this out to DiscordRestClient
                using (var client = new HttpClient())
                {
                    var message = new CreateMessageRest()
                    {
                        Content = eventData.Content.Substring(origCommand.Length),
                        Tts = false
                    };

                    var postData = JsonConvert.SerializeObject(message);

                    client.BaseAddress = new Uri("https://discordapp.com/api/");
                    client.DefaultRequestHeaders.Add("User-Agent", "DiscordBot (SleepyBot, 0.1)");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bot {_userService.DiscordAuthInfo.Token}");

                    var response = await client.PostAsync(
                         $"channels/{channelId}/messages",
                         new StringContent(postData, Encoding.UTF8, "application/json"));
                }
            }
        }
    }
}
