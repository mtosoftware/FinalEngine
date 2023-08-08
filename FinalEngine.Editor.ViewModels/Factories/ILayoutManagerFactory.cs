// <copyright file="ILayoutSerializerFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Factories;

using FinalEngine.Editor.ViewModels.Interactions;

public interface ILayoutManagerFactory
{
    ILayoutManager CreateManager();
}
