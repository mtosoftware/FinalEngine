// <copyright file="Factory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Factories;

using System;

/// <summary>
/// Provides a standard implementation of an <see cref="IFactory{T}"/>.
/// </summary>
/// <typeparam name="T">
/// The type of instance to crate.
/// </typeparam>
/// <seealso cref="IFactory{T}"/>
public sealed class Factory<T> : IFactory<T>
{
    /// <summary>
    /// The function required to create the instance.
    /// </summary>
    private readonly Func<T> factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="Factory{T}"/> class.
    /// </summary>
    /// <param name="factory">
    /// The function used when creating the required instance.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="factory"/> parameter cannot be null.
    /// </exception>
    public Factory(Func<T> factory)
    {
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    /// <inheritdoc/>
    public T Create()
    {
        return this.factory();
    }
}
