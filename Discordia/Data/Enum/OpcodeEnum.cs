using System;
using System.Collections.Generic;
using System.Text;

namespace Discordia.Data.Enum
{
    public enum DiscordOpcodeEnum
    {
        Dispatch,
        Heartbeat,
        Identify,
        StatusUpdate,
        VoiceStateUpdate,
        Null,
        Resume,
        Reconnect,
        RequestGuildMembers,
        InvalidSession,
        Hello,
        HeartbeatAck
    }
}
