﻿namespace FinalEngine.Platform
{
    /// <summary>
    ///   Defines an interface that provides a method for processing OS-specific events.
    /// </summary>
    public interface IEventsProcessor
    {
        /// <summary>
        ///   Processes the events that are currently in the message queue.
        /// </summary>
        void ProcessEvents();
    }
}