// <copyright file="IEventsProcessor.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform;

/// <summary>
///   Defines an interface that provides a method for processing OS-specific events in a message queue.
/// </summary>
public interface IEventsProcessor
{
    /// <summary>
    ///   Processes the events that are currently in the message queue.
    /// </summary>
    void ProcessEvents();
}
