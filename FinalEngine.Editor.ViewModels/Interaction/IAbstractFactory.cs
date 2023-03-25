// <copyright file="IAbstractFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction;

/// <summary>
/// Defines an interface that provides a method to create an instance of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">
/// The type of instance to create.
/// </typeparam>
public interface IAbstractFactory<T>
{
    /// <summary>
    /// Creates an instance of type <typeparamref name="T"/>.
    /// </summary>
    /// <returns>
    /// The newly created instance of type <typeparamref name="T"/>.
    /// </returns>
    T Create();
}
