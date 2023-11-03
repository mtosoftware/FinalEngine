// <copyright file="IMementoCaretaker.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Commands;

/// <summary>
/// Defines an interface that represents a memento caretaker, used to apply and revert a collection of mementos.
/// </summary>
public interface IMementoCaretaker
{
    bool CanRedo { get; }

    bool CanUndo { get; }

    void Apply(IMementoCommand command);

    void Redo();

    void Undo();
}
