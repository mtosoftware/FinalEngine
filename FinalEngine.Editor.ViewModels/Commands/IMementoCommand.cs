// <copyright file="IMementoCommand.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Commands;

/// <summary>
/// Defines an interface that represents a command that be applied and reverted.
/// </summary>
/// <remarks>
/// You should implement this interface when you wish to perform an action that should be managed by a caretaker (for example, an action that is part of an undo/redo system).
/// </remarks>
public interface IMementoCommand
{
    /// <summary>
    /// Gets the name of the action.
    /// </summary>
    /// <value>
    /// The name of the action.
    /// </value>
    string ActionName { get; }

    /// <summary>
    /// Applies this action (in other words, executes the command).
    /// </summary>
    void Apply();

    /// <summary>
    /// Reverts this action (in other words, performs the reverse action of <see cref="Apply"/>).
    /// </summary>
    void Revert();
}
