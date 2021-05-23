// <copyright file="PlatformResolver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using FinalEngine.Launching.Factories;
    using FinalEngine.Launching.Invocation;

    public class PlatformResolver : IPlatformResolver
    {
        private static IPlatformResolver instance;

        private readonly IDictionary<OSPlatform, IGamePlatformFactory> platformToFactoryMap;

        private readonly IRuntimeInformationInvoker runtime;

        [ExcludeFromCodeCoverage]
        public PlatformResolver()
            : this(new RuntimeInformationInvoker())
        {
            var desktop = new DesktopGamePlatformFactory();

            this.Register(OSPlatform.Windows, desktop);
            this.Register(OSPlatform.OSX, desktop);
            this.Register(OSPlatform.Linux, desktop);
        }

        public PlatformResolver(IRuntimeInformationInvoker runtime)
        {
            this.runtime = runtime ?? throw new ArgumentNullException(nameof(runtime), $"The specified {nameof(runtime)} parameter cannot be null.");

            this.platformToFactoryMap = new Dictionary<OSPlatform, IGamePlatformFactory>();
        }

        [ExcludeFromCodeCoverage]
        public static IPlatformResolver Instance
        {
            get { return instance ??= new PlatformResolver(); }
        }

        public void Register(OSPlatform platform, IGamePlatformFactory factory, bool remove = false)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory), $"The specified {nameof(factory)} parameter cannot be null.");
            }

            if (this.platformToFactoryMap.ContainsKey(platform))
            {
                if (!remove)
                {
                    throw new ArgumentException($"The specified {nameof(platform)} parameter cannot be used as a factory for that platform has already been registered. If you wish to override this, set the optional {nameof(remove)} parameter to true.");
                }

                this.platformToFactoryMap.Remove(platform);
            }

            this.platformToFactoryMap.Add(platform, factory);
        }

        public IGamePlatformFactory Resolve()
        {
            foreach (KeyValuePair<OSPlatform, IGamePlatformFactory> kvp in this.platformToFactoryMap)
            {
                if (this.runtime.IsOSPlatform(kvp.Key))
                {
                    return kvp.Value;
                }
            }

            throw new PlatformNotSupportedException($"The current platform is not supported: {RuntimeInformation.OSDescription}. You can add support by using the {nameof(this.Register)} function.");
        }
    }
}