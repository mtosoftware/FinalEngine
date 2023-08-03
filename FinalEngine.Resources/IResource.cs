// <copyright file="IResource.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System;
using System.Diagnostics.CodeAnalysis;

/// <summary>
///   Defines an interface that represents a resource that can be loaded by a <see cref="ResourceLoaderBase{T}"/>.
/// </summary>
/// <remarks>
/// Implements this interface on resources that you wish to be managed by the <see cref="ResourceManager"/>.
/// </remarks>
/// <seealso cref="IDisposable"/>
[SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Required for Resource Manager.")]
public interface IResource
{
}
