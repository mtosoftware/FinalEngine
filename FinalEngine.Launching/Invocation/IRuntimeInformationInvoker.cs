// <copyright file="IRuntimeInformationInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching.Invocation
{
    using System.Runtime.InteropServices;

    /// <summary>
    ///   Defines an interface that provides methods for of the <see cref="RuntimeInformation"/> class.
    /// </summary>
    public interface IRuntimeInformationInvoker
    {
        /// <inheritdoc cref="RuntimeInformation.IsOSPlatform(OSPlatform)"/>
        bool IsOSPlatform(OSPlatform platform);
    }
}