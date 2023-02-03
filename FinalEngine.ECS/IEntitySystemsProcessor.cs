// <copyright file="IEntitySystemsProcessor.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS;

/// <summary>
///   Defines an interface that provides a function to process all entity systems for a given execution type.
/// </summary>
public interface IEntitySystemsProcessor
{
    /// <summary>
    ///   Processes all entity systems that match the given execution <paramref name="type"/>.
    /// </summary>
    /// <param name="type">
    ///   The execution type.
    /// </param>
    void ProcessAll(GameLoopType type);
}
