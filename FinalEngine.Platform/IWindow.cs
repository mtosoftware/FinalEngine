// <copyright file="IWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform
{
    using System;
    using System.Drawing;

    /// <summary>
    ///   Defines an interface that represents a display or window.
    /// </summary>
    /// <seealso cref="IDisposable"/>
    public interface IWindow : IDisposable
    {
        /// <summary>
        ///   Gets the size of the client area of this <see cref="IWindow"/>.
        /// </summary>
        /// <value>
        ///   The size of the client area of this <see cref="IWindow"/>.
        /// </value>
        Size ClientSize { get; }

        /// <summary>
        ///   Gets a value indicating whether this <see cref="IWindow"/> is exiting.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this <see cref="IWindow"/> is exiting; otherwise, <c>false</c>.
        /// </value>
        bool IsExiting { get; }

        /// <summary>
        ///   Gets a value indicating whether this <see cref="IWindow"/> is focused.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this <see cref="IWindow"/> is focused; otherwise, <c>false</c>.
        /// </value>
        bool IsFocused { get; }

        /// <summary>
        ///   Gets or sets the size of this <see cref="IWindow"/>.
        /// </summary>
        /// <value>
        ///   The size of this <see cref="IWindow"/>.
        /// </value>
        Size Size { get; set; }

        /// <summary>
        ///   Gets or sets the title of this <see cref="IWindow"/>.
        /// </summary>
        /// <value>
        ///   The title of this <see cref="IWindow"/>.
        /// </value>
        string Title { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether this <see cref="IWindow"/> is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this <see cref="IWindow"/> is visible; otherwise, <c>false</c>.
        /// </value>
        bool Visible { get; set; }

        /// <summary>
        ///   Closes this <see cref="IWindow"/>, disposing of it's resources.
        /// </summary>
        void Close();
    }
}