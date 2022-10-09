// <copyright file="IResourceLoaderRegistrar.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Services.Resources
{
    /// <summary>
    ///   Defines an interface that provides a method for registering resource loaders to a resource manager.
    /// </summary>
    public interface IResourceLoaderRegistrar
    {
        /// <summary>
        ///   Registers all resource loaders to the resource manager.
        /// </summary>
        void RegisterAll();
    }
}
