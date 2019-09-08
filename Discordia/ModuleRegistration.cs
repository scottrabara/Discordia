using Autofac;
using Discordia.Network;
using Discordia.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discordia
{
    static public class ModuleRegistration
    {
        static public void Register(DiscordConnection connection)
        {
            var builder = new ContainerBuilder();

            var loggerFactory = LoggerFactory.Create((config) =>
            {
                // Configure logger
                config.AddConsole();
            });

            builder.Register(c => loggerFactory).As<ILoggerFactory>();

            // DiscordConnection injection
            builder.RegisterInstance(connection)
                .OnActivated(h =>
                {
                    connection.handlerService = h.Context.Resolve<DispatchHandlerService>();
                    connection.Logger = h.Context.Resolve<ILoggerFactory>().CreateLogger<DiscordConnection>();
                    connection.userService = h.Context.Resolve<UserService>();
                });

            // DispatchHandlerService resolve
            builder.Register(c => 
            {
                var logger = c.Resolve<ILoggerFactory>().CreateLogger<DispatchHandlerService>();
                var messageCreateHandlerService = c.Resolve<MessageCreateHandlerService>();
                var userService = c.Resolve<UserService>();
                return new DispatchHandlerService(logger, messageCreateHandlerService, userService);
            }).InstancePerLifetimeScope();

            // MessageCreateHandlerService resolve
            builder.Register(c =>
            {
                var logger = c.Resolve<ILoggerFactory>().CreateLogger<MessageCreateHandlerService>();
                var userService = c.Resolve<UserService>();
                return new MessageCreateHandlerService(logger, userService);
            }).InstancePerLifetimeScope();

            // UserService resolve
            builder.Register(c =>
            {
                var logger = c.Resolve<ILoggerFactory>().CreateLogger<UserService>();
                return new UserService(logger);
            }).InstancePerLifetimeScope();

            // Build container
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                scope.Resolve<DiscordConnection>();
            }
        }
    }
}
