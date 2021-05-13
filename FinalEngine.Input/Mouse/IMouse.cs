// <copyright file="IMouse.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouse
{
    using System.Drawing;

    /// <summary>
    ///   Defines an interface that provides real time handling of mouse operations.
    /// </summary>
    public interface IMouse
    {
        /// <summary>
        ///   Gets the location of the cursor during the previous frame.
        /// </summary>
        /// <value>
        ///   The location of cursor during the previous frame.
        /// </value>
        PointF Delta { get; }

        /// <summary>
        ///   Gets or sets the location of the cursor in window pixel coordinates.
        /// </summary>
        /// <value>
        ///   The location of the cursor in window pixel coordinates.
        /// </value>
        PointF Location { get; set; }

        /// <summary>
        ///   Gets the scroll wheel offset.
        /// </summary>
        /// <value>
        ///   The scroll wheel offset.
        /// </value>
        double WheelOffset { get; }

        /// <summary>
        ///   Determines whether the specified <paramref name="button"/> is currently down.
        /// </summary>
        /// <param name="button">
        ///   Specifies a <see cref="MouseButton"/> that represents the button to check for.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="button"/> is currently down; otherwise, <c>false</c>.
        /// </returns>
        bool IsButtonDown(MouseButton button);

        /// <summary>
        ///   Determines whether the specified <paramref name="button"/> has been pressed this frame.
        /// </summary>
        /// <param name="button">
        ///   Specifies a <see cref="MouseButton"/> that represents the button to check for.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="button"/> has been pressed this frame; otherwise, <c>false</c>.
        /// </returns>
        bool IsButtonPressed(MouseButton button);

        /// <summary>
        ///   Determines whether the specified <paramref name="button"/> has been released since the previous frame.
        /// </summary>
        /// <param name="button">
        ///   Specifies a <see cref="MouseButton"/> that represents the button to check for.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the specified <paramref name="button"/> has been released since the previous frame; otherwise, <c>false</c>.
        /// </returns>
        bool IsButtonReleased(MouseButton button);

        /// <summary>
        ///   Updates this <see cref="IMouse"/>.
        /// </summary>
        void Update();
    }
}