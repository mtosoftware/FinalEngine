// <copyright file="IResourceLoaderInternal.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

/// <summary>
///   Defines an interface that provides a method for loading a resource.
/// </summary>
internal interface IResourceLoaderInternal
{
    /// <summary>
    ///   Loads a resource from the specified <paramref name="filePath"/>.
    /// </summary>
    /// <param name="filePath">
    ///   The file path to load the resource from.
    /// </param>
    /// <returns>
    ///   The newly loaded resource.
    /// </returns>
    IResource LoadResource(string filePath);
}
