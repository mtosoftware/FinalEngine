// <copyright file="IGameTime.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform
{
    /// <summary>
    ///   Defines an interface that provides a method for checking whether the next frame can be processed and rendered.
    /// </summary>
    public interface IGameTime
    {
        /// <summary>
        ///   Determines whether the next frame can be processed and rendered.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can process the next frame; otherwise, <c>false</c>.
        /// </returns>
        bool CanProcessNextFrame();
    }
}