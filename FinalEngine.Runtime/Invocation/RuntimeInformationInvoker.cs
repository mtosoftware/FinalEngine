// <copyright file="RuntimeInformationInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Invocation;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

[ExcludeFromCodeCoverage(Justification = "Invocation")]
internal sealed class RuntimeInformationInvoker : IRuntimeInformationInvoker
{
    public bool IsOSPlatform(OSPlatform platform)
    {
        return RuntimeInformation.IsOSPlatform(platform);
    }
}
