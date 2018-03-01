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

        public static ISignalRBuilder AddSignalR(this IServiceCollection services, Action<HubOptions> options)
        {
            services.AddSingleton<IConfigureOptions<HubOptions>, HubOptionsSetup>()
                .Configure(options);
            return services.AddSignalR();
            
        }
    }
}
