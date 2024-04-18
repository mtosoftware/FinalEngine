// <copyright file="IResourceLoader.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System;

public interface IResourceLoader
{
    Type GetResourceType();

    IResource LoadResource(string filePath);
}
