// <copyright file="IResource.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Defines an interface that represents a resource that can loaded via an <see cref="IResourceManager"/>.
/// </summary>
///
/// <remarks>
/// You should implement <see cref="IResource"/> on an <c>object</c> that should be managed by an <see cref="IResourceManager"/>.
/// </remarks>
[SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Required for Resource Manager.")]
public interface IResource
{
}
