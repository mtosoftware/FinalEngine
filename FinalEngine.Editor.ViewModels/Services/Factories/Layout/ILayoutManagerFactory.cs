// <copyright file="ILayoutManagerFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Services.Factories.Layout;

using FinalEngine.Editor.ViewModels.Services.Layout;

public interface ILayoutManagerFactory
{
    ILayoutManager CreateManager();
}
