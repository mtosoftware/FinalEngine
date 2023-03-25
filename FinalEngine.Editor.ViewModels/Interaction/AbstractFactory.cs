// <copyright file="AbstractFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction;

using System;

public class AbstractFactory<T> : IAbstractFactory<T>
{
    private readonly Func<T> factory;

    public AbstractFactory(Func<T> factory)
    {
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public T Create()
    {
        return this.factory();
    }
}
