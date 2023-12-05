// <copyright file="Factory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Factories;

using System;

public sealed class Factory<T> : IFactory<T>
{
    private readonly Func<T> factory;

    public Factory(Func<T> factory)
    {
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public T Create()
    {
        return this.factory();
    }
}
