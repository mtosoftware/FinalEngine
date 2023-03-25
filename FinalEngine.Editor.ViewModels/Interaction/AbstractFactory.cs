// <copyright file="AbstractFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction;

using System;

/// <summary>
/// Provides a standard implementation of an <see cref="IAbstractFactory{T}"/>.
/// </summary>
/// <typeparam name="T">
/// The type of instance to create.
/// </typeparam>
/// <seealso cref="IAbstractFactory{T}"/>
public class AbstractFactory<T> : IAbstractFactory<T>
{
    /// <summary>
    /// The function used to create the instance.
    /// </summary>
    private readonly Func<T> factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbstractFactory{T}"/> class.
    /// </summary>
    /// <param name="factory">
    /// The function used to create the instance.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="factory"/> parameter cannot be null.
    /// </exception>
    public AbstractFactory(Func<T> factory)
    {
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    /// <inheritdoc/>
    public T Create()
    {
        return this.factory();
    }
}
