// <copyright file="GameLoopType.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS
{
    /// <summary>
    ///   Enumerates the available game loop type for a system.
    /// </summary>
    public enum GameLoopType
    {
        /// <summary>
        ///   The process will be handled per update.
        /// </summary>
        Update,

        /// <summary>
        /// The process will be handled per render.
        /// </summary>
        Render,
    }
}
