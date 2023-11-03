// <copyright file="MementoCaretaker.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Commands;

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

public sealed class MementoCaretaker : IMementoCaretaker
{
    private readonly ILogger<MementoCaretaker> logger;

    private readonly Stack<IMementoCommand> redoStack;

    private readonly Stack<IMementoCommand> undoStack;

    public MementoCaretaker(ILogger<MementoCaretaker> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        this.undoStack = new Stack<IMementoCommand>();
        this.redoStack = new Stack<IMementoCommand>();
    }

    public bool CanRedo
    {
        get { return this.redoStack.Count > 0; }
    }

    public bool CanUndo
    {
        get { return this.undoStack.Count > 0; }
    }

    public void Apply(IMementoCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        this.logger.LogInformation($"Applying '{command.ActionName}'...");

        command.Apply();
        this.undoStack.Push(command);
    }

    public void Redo()
    {
        if (!this.CanRedo)
        {
            return;
        }

        var memento = this.redoStack.Pop();

        if (memento == null)
        {
            return;
        }

        this.Apply(memento);
    }

    public void Undo()
    {
        if (!this.CanUndo)
        {
            return;
        }

        var memento = this.undoStack.Pop();

        if (memento == null)
        {
            return;
        }

        this.logger.LogInformation($"Reverting '{memento.ActionName}'...");

        memento.Revert();
        this.redoStack.Push(memento);
    }
}
