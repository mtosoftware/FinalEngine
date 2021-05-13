// <copyright file="PlatformResolver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class PlatformResolver : IPlatformResolver
    {
        private static IPlatformResolver instance;

        private readonly IDictionary<OSPlatform, IGamePlatformFactory> platformToFactoryMap;

        public PlatformResolver()
        {
            this.platformToFactoryMap = new Dictionary<OSPlatform, IGamePlatformFactory>();
        }

        public static IPlatformResolver Instance
        {
            get { return instance ??= new PlatformResolver(); }
        }

        public void Register<TFactory>(OSPlatform platform, bool remove = false)
            where TFactory : IGamePlatformFactory, new()
        {
            if (this.platformToFactoryMap.ContainsKey(platform))
            {
                if (!remove)
                {
                    throw new ArgumentException($"The specified {nameof(platform)} parameter cannot be used as a factory for that platform has already been registered. If you wish to override this, set the optional {nameof(remove)} parameter to true.");
                }

                this.platformToFactoryMap.Remove(platform);
            }

            this.platformToFactoryMap.Add(platform, new TFactory());
        }

        public IGamePlatformFactory Resolve()
        {
            foreach (KeyValuePair<OSPlatform, IGamePlatformFactory> kvp in this.platformToFactoryMap)
            {
                if (RuntimeInformation.IsOSPlatform(kvp.Key))
                {
                    return kvp.Value;
                }
            }

            throw new PlatformNotSupportedException($"The current platform is not supported: {RuntimeInformation.OSDescription}. You can add support by using the {nameof(this.Register)} function.");
        }
    }
}