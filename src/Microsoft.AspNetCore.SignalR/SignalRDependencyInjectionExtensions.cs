// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Internal.Protocol;
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

        public static ISignalRBuilder AddGlobalHubOptions(this ISignalRBuilder signalRBuilder, Action<HubOptions> options)
        {
            signalRBuilder.Services.Configure(options);
            return signalRBuilder;
        }

        public static ISignalRBuilder AddHubOptions<THub>(this ISignalRBuilder signalrBuilder, Action<HubOptions<THub>> options) where THub : Hub
        {
            signalrBuilder.Services.Configure(options);
            return signalrBuilder;
        }
    }

    public class HubOptionsSetup : IConfigureOptions<HubOptions> 
    {
        private readonly List<string> _protocols = new List<string>();

        public HubOptionsSetup(IEnumerable<IHubProtocol> protocols)
        {
            foreach (IHubProtocol hubProtocol in protocols)
            {
                _protocols.Add(hubProtocol.Name);
            }
        }
        public void Configure(HubOptions options)
        {
            options.SupportedProtocols.AddRange(_protocols);
        }
    }

    public class HubOptionsSetup<THub> : IConfigureOptions<HubOptions<THub>> where THub : Hub
    {
        private readonly HubOptions _hubOptions;
        public HubOptionsSetup(IOptions<HubOptions> options)
        {
            _hubOptions = options.Value;
        }

        public void Configure(HubOptions<THub> options)
        {
            options.SupportedProtocols.AddRange(_hubOptions.SupportedProtocols);
            options.KeepAliveInterval = _hubOptions.KeepAliveInterval;
            options.NegotiateTimeout = _hubOptions.NegotiateTimeout;
        }
    }
}
