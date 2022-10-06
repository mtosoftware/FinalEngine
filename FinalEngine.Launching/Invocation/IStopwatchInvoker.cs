// <copyright file="IStopwatchInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Invocation
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///   Defines an interface that provides methods for invocation of a <see cref="Stopwatch"/>.
    /// </summary>
    public interface IStopwatchInvoker
    {
        /// <inheritdoc cref="Stopwatch.Elapsed"/>
        TimeSpan Elapsed { get; }

        /// <inheritdoc cref="Stopwatch.IsRunning"/>
        bool IsRunning { get; }

        /// <inheritdoc cref="Stopwatch.Restart"/>
        void Restart();
    }
}