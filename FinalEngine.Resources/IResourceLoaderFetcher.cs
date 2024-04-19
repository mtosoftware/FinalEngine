// <copyright file="IResourceLoaderFetcher.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Resources;

using System.Collections.Generic;

public interface IResourceLoaderFetcher
{
    IEnumerable<IResourceLoader> GetResourceLoaders();
}
