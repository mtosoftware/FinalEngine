// <copyright file="IFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Factories;

using FinalEngine.Editor.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Defines an interface that provides a generic factory method.
/// </summary>
/// <typeparam name="T">
/// The type of instance to create.
/// </typeparam>
/// <remarks>
/// This interface is typically used alongside <see cref="ServiceCollectionExtensions.AddFactory{TService, TImplementation}(IServiceCollection)"/> exteension to provide a factory for view models.
/// </remarks>
public interface IFactory<out T>
{
    /// <summary>
    /// Creates an instance of type <typeparamref name="T"/>.
    /// </summary>
    /// <returns>
    /// The newly created <typeparamref name="T"/> instance.
    /// </returns>
    T Create();
}
