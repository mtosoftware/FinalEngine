// <copyright file="IEntitySystemsProcessor.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS
{
    /// <summary>
    ///   Defines an interface that provides a method for processing all systems of a specified <see cref="GameLoopType"/>.
    /// </summary>
    public interface IEntitySystemsProcessor
    {
        /// <summary>
        ///   Processes all systems that match the specified loop <paramref name="type"/>.
        /// </summary>
        /// <param name="type">
        ///   The loop type of systems to process.
        /// </param>
        void ProcessAll(GameLoopType type);
    }
}