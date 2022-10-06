// <copyright file="RuntimeInformationInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Invocation
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IRuntimeInformationInvoker"/>.
    /// </summary>
    /// <seealso cref="IRuntimeInformationInvoker"/>
    [ExcludeFromCodeCoverage(Justification = "Invocation Class")]
    public class RuntimeInformationInvoker : IRuntimeInformationInvoker
    {
        /// <inheritdoc/>
        public bool IsOSPlatform(OSPlatform platform)
        {
            return RuntimeInformation.IsOSPlatform(platform);
        }
    }
}