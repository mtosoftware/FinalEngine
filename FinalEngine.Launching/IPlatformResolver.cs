// <copyright file="IPlatformResolver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    using System.Runtime.InteropServices;

    /// <summary>
    ///   Defines an interface that provides methods for registering and resolving game resource factories for the current platform.
    /// </summary>
    public interface IPlatformResolver
    {
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
        void Register(OSPlatform platform, IGamePlatformFactory factory, bool remove = false);

        /// <summary>
        ///   Resolves the current platform by returning it's corresponding <see cref="IGamePlatformFactory"/>.
        /// </summary>
        /// <returns>
        ///   A <see cref="IGamePlatformFactory"/> for the current platform.
        /// </returns>
        IGamePlatformFactory Resolve();
    }
}