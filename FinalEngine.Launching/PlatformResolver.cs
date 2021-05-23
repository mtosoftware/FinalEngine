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

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IPlatformResolver"/> using an <see cref="IRuntimeInformationInvoker"/> to check for the current platform.
    /// </summary>
    /// <seealso cref="FinalEngine.Launching.IPlatformResolver"/>
    public class PlatformResolver : IPlatformResolver
    {
        /// <summary>
        ///   The initial size capacity of the <see cref="platformToFactoryMap"/> dictionary.
        /// </summary>
        private const int InitialSizeCapacity = 10;

        /// <summary>
        ///   The instance.
        /// </summary>
        private static IPlatformResolver? instance;

        /// <summary>
        ///   The platform to factory map.
        /// </summary>
        private readonly IDictionary<OSPlatform, IGamePlatformFactory> platformToFactoryMap;

        /// <summary>
        ///   The runtime information invoker, used to check the current platform.
        /// </summary>
        private readonly IRuntimeInformationInvoker runtime;

        /// <summary>
        ///   Initializes a new instance of the <see cref="PlatformResolver"/> class.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public PlatformResolver()
            : this(new RuntimeInformationInvoker())
        {
            var desktop = new DesktopGamePlatformFactory();

            this.Register(OSPlatform.Windows, desktop);
            this.Register(OSPlatform.OSX, desktop);
            this.Register(OSPlatform.Linux, desktop);
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="PlatformResolver"/> class.
        /// </summary>
        /// <param name="runtime">
        ///   The runtime information invoker, used to check the current platform.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="runtime"/> parameter cannot be null.
        /// </exception>
        public PlatformResolver(IRuntimeInformationInvoker runtime)
        {
            this.runtime = runtime ?? throw new ArgumentNullException(nameof(runtime), $"The specified {nameof(runtime)} parameter cannot be null.");

            this.platformToFactoryMap = new Dictionary<OSPlatform, IGamePlatformFactory>(InitialSizeCapacity);
        }

        /// <summary>
        ///   Gets the instance.
        /// </summary>
        /// <value>
        ///   The instance.
        /// </value>
        [ExcludeFromCodeCoverage]
        public static IPlatformResolver Instance
        {
            get { return instance ??= new PlatformResolver(); }
        }

        /// <summary>
        ///   Registers the specified <paramref name="factory"/> for use with the specified <paramref name="platform"/>.
        /// </summary>
        /// <param name="platform">
        ///   The platform in which the factory will be resolved when using the <see cref="Resolve"/> method.
        /// </param>
        /// <param name="factory">
        ///   The factory that will be used to create resources for the specified <paramref name="platform"/>.
        /// </param>
        /// <param name="remove">
        ///   if set to <c>true</c> the specified <paramref name="factory"/> will override the specified <paramref name="platform"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="factory"/> parameter cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///   The specified <paramref name="platform"/> parameter cannot be used as a factory for that platform has already been registered. If you wish to override the factory, set the optional <paramref name="remove"/> parameter to true.
        /// </exception>
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

        /// <summary>
        ///   Resolves the current platform by returning it's corresponding <see cref="IGamePlatformFactory"/>.
        /// </summary>
        /// <returns>
        ///   A <see cref="IGamePlatformFactory"/> for the current platform.
        /// </returns>
        /// <exception cref="PlatformNotSupportedException">
        ///   The current platform is not supported: {RuntimeInformation.OSDescription}. You can add support by using the {nameof(this.Register)} function.
        /// </exception>
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