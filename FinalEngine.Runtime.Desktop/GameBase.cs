// <copyright file="GameBase.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Desktop;

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
