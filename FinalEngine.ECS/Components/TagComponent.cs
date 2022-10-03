// <copyright file="TagComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components
{
    /// <summary>
    ///   Provides a component that represents a tag or name.
    /// </summary>
    /// <seealso cref="IComponent"/>
    public class TagComponent : IComponent
    {
        /// <summary>
        ///   Gets or sets the tag.
        /// </summary>
        /// <value>
        ///   The tag (or <c>null</c> if not defined).
        /// </value>
        public string? Tag { get; set; }
    }
}