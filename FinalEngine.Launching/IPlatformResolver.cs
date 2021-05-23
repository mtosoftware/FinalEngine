// <copyright file="IPlatformResolver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    using System.Runtime.InteropServices;

    public interface IPlatformResolver
    {
        void Register(OSPlatform platform, IGamePlatformFactory factory, bool remove = false);

        IGamePlatformFactory Resolve();
    }
}