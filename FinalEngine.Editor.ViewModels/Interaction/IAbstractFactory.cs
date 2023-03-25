// <copyright file="IAbstractFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction;

public interface IAbstractFactory<T>
{
    T Create();
}
