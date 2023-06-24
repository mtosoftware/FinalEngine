// <copyright file="GameBase.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Desktop;

/// <summary>
/// Provides a desktop implementation of a <see cref="GameContainerBase"/>.
/// </summary>
/// <seealso cref="GameContainerBase" />
public abstract class GameBase : GameContainerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GameBase"/> class.
    /// </summary>
    protected GameBase()
        : base(new DesktopRuntimeFactory())
    {
    }
}
