using Discordia.Network;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {

        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                var client = new DiscordConnection();

                await client.ConnectAsync("...");

            });

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    break;
            }
        }
    }
}
