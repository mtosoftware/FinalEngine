// <copyright file="EditorUpdateEntitySystem.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Driver.Systems;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.ECS;
using FinalEngine.ECS.Attributes;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;

[EntitySystemProcess(ExecutionType = GameLoopType.Update)]
public sealed class EditorUpdateEntitySystem : EntitySystemBase
{
    private readonly IKeyboard keyboard;

    private readonly IMouse mouse;

    public EditorUpdateEntitySystem(IKeyboard keyboard, IMouse mouse)
    {
        this.keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
        this.mouse = mouse ?? throw new ArgumentNullException(nameof(mouse));
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        this.keyboard.Update();
        this.mouse.Update();
    }
}
