using Autofac;
using Discordia.Data;
using Discordia.Data.Enum;
using Discordia.Data.EventData;
using Discordia.Data.PartialData;
using Discordia.Data.User;
using Discordia.Service;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Discordia.Network
{
    public class DiscordConnection
    {
        public DispatchHandlerService handlerService { get; set; }
        public UserService userService { get; set; }
        public ILogger<DiscordConnection> Logger { get; set; }
        public string Token { get; set; }

        ClientWebSocket _client;
        int? _lastSequence;
        int _heartbeatInterval;
        public DiscordConnection()
        {
            ModuleRegistration.Register(this);
            _client = new ClientWebSocket();
            _lastSequence = 0;
        }

        public async Task ConnectAsync(string token)
        {
            Logger.LogInformation("Connecting");
            await _client.ConnectAsync(new Uri("wss://gateway.discord.gg/?v=6&encoding=json"), CancellationToken.None);

            var buffer = new ArraySegment<byte>(new byte[1028]);
            await _client.ReceiveAsync(buffer, CancellationToken.None);

            var response = Encoding.UTF8.GetString(buffer);
            var responseObj = JsonConvert.DeserializeObject<GatewayPayload>(response);

            _lastSequence = responseObj.Sequence;
            _heartbeatInterval = responseObj.EventData.Value<int>("heartbeat_interval");

            Logger.LogInformation("Connected");

            StartListening();
            StartHeartbeat();
            SendIdentify(token);
            userService.DiscordAuthInfo.Token = token;
        }

        public void SendIdentify(string token)
        {
            Task.Factory.StartNew(async () =>
            {
                var identifyPayload = new IdentifyEventData()
                {
                    Token = token,
                    Properties = new IdentifyProperties()
                    {
                        OperatingSystem = "windows",
                        Browser = "test_lib",
                        Device = "test_lib"
                    }
                };

                var identifyPayloadJson = JsonConvert.SerializeObject(identifyPayload);

                var identifyRequest = new GatewayPayload()
                {
                    Opcode = DiscordOpcodeEnum.Identify,
                    EventData = JToken.FromObject(identifyPayload)
                };

                await SendAsync(identifyRequest);
            });
        }

        public void StartHeartbeat()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Task.Delay(_heartbeatInterval);
                    var heartbeatRequest = new GatewayPayload()
                    {
                        Opcode = DiscordOpcodeEnum.Heartbeat,
                        EventData = new JValue(_lastSequence)
                    };

                    await SendAsync(heartbeatRequest);
                }
            });
        }

        public void StartListening()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var buffer = new ArraySegment<byte>(new byte[1028 * 8]);
                    await _client.ReceiveAsync(buffer, CancellationToken.None);

                    var response = Encoding.UTF8.GetString(buffer);
                    var payload = JsonConvert.DeserializeObject<GatewayPayload>(response);
                    var name = Enum.GetName(typeof(DiscordOpcodeEnum), payload.Opcode);

                    await handlerService.HandleAsync(payload);

                    if (payload.Opcode != DiscordOpcodeEnum.HeartbeatAck)
                        _lastSequence = payload.Sequence;
                }
            });
        }

        public async Task SendAsync(GatewayPayload payload)
        {
            var name = Enum.GetName(typeof(DiscordOpcodeEnum), payload.Opcode);

            var jsonString = JsonConvert.SerializeObject(payload);

            var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonString));

            await _client.SendAsync(
                buffer,
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
        }
    }
}
