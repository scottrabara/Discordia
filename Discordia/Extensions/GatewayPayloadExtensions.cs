using Discordia.Abstractions;
using Discordia.Data;
using Discordia.Data.Enum;
using Discordia.Data.EventData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discordia.Extensions
{
    public static class GatewayPayloadExtensions
    {
        public static IEventData GetEventData(this GatewayPayload payload)
        {
            IEventData result = null;
            if (payload.EventName == EventNameConst.MESSAGE_CREATE)
                result = JsonConvert.DeserializeObject<MessageCreateEventData>(payload.EventData.ToString());
            if (payload.EventName == EventNameConst.READY)
                result = JsonConvert.DeserializeObject<ReadyEventData>(payload.EventData.ToString());

            return result;
        }
    }
}
