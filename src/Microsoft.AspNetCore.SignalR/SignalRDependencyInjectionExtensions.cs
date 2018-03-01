// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SignalRDependencyInjectionExtensions
    {
        public static ISignalRBuilder AddSignalR(this IServiceCollection services)
        {
            services.AddSockets();
            return services.AddSignalRCore()
                .AddJsonProtocol();
        }

        public static ISignalRBuilder AddGlobalHubOptions(this ISignalRBuilder signalrBuilder, Action<HubOptions> options)
        {
            signalrBuilder.Services.AddSingleton<IConfigureOptions<HubOptions>, HubOptionsSetup>();
            signalrBuilder.Services.Configure(options);
            return signalrBuilder;
        }

        public static ISignalRBuilder AddHubOptions<THub>(this ISignalRBuilder signalrBuilder, Action<HubOptions<THub>> options) where THub : Hub
        {
            signalrBuilder.Services.AddSingleton(typeof(IConfigureOptions<HubOptions<THub>>), typeof(HubOptionsSetup<THub>));
            signalrBuilder.Services.Configure(options);

            return signalrBuilder;
        }
    }
}
