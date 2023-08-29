// <copyright file="IMouse.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System.Drawing;

/// <summary>
/// Represents an interface that defines a mouse that allows interaction mouse and scroll wheel state.
/// </summary>
///
/// <remarks>
/// The <see cref="IMouse"/> interface provides properties and methods to query the current state of a mouse including it's location, button states and scroll wheel offset.
/// </remarks>
public interface IMouse
{
    /// <summary>
    /// Gets the change in position of the cursor during the previous frame.
    /// </summary>
    ///
    /// <value>
    /// The change in position of cursor during the previous frame.
    /// </value>
    PointF Delta { get; }

    /// <summary>
    /// Gets or sets the location of the cursor in window pixel coordinates.
    /// </summary>
    ///
    /// <value>
    /// The location of the cursor in window pixel coordinates.
    /// </value>
    ///
    /// <example>
    /// Below you'll find an example showing how to modify the location of the cursor. This example assumes that an implementation of <see cref="IMouse"/> has been provided, such as <see cref="Mouse"/>.
    ///
    /// <code>
    /// var isReleased = mouse.IsButtonReleased(MouseButton.Left);
    ///
    /// if (isReleased)
    /// {
    ///     // Modify the location of the cursor in window pixel coordinates.
    ///     mouse.Location = new PointF(100.0f, 100.0f);
    /// }
    /// </code>
    /// </example>
    PointF Location { get; set; }

    /// <summary>
    /// Gets the scroll wheel offset.
    /// </summary>
    ///
    /// <value>
    /// The scroll wheel offset.
    /// </value>
    double WheelOffset { get; }

    /// <summary>
    /// Determines whether the specified <paramref name="button"/> is down during the current iteration.
    /// </summary>
    ///
    /// <param name="button">
    /// Specifies a <see cref="MouseButton"/> that represents the button to check.
    /// </param>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if a button is being held down. This example assumes that an implementation of <see cref="IMouse"/> has been provided, such as <see cref="Mouse"/>.
    ///
    /// <code>
    /// var isDown = mouse.IsButtonDown(MouseButton.Right);
    ///
    /// if (isDown)
    /// {
    ///     Console.WriteLine("The left mouse button is held down.");
    /// }
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// <c>true</c> if the specified <paramref name="button"/> is down during the current iteration; otherwise, <c>false</c>.
    /// </returns>
    bool IsButtonDown(MouseButton button);

    /// <summary>
    /// Determines whether the specified <paramref name="button"/> has been pressed this iteration.
    /// </summary>
    ///
    /// <param name="button">
    /// Specifies a <see cref="MouseButton"/> that represents the button to check.
    /// </param>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if a button has been pressed during the current iteration. This example assumes that an implementation of <see cref="IMouse"/> has been provided, such as <see cref="Mouse"/>.
    ///
    /// <code>
    /// var isPressed = mouse.IsButtonPressed(MouseButton.Middle);
    ///
    /// if (isPressed)
    /// {
    ///     Console.WriteLine("The middle button has been pressed.");
    /// }
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// <c>true</c> if the specified <paramref name="button"/> has been pressed this iteration; otherwise, <c>false</c>.
    /// </returns>
    bool IsButtonPressed(MouseButton button);

    /// <summary>
    /// Determines whether the specified <paramref name="button"/> has been released since the previous iteration.
    /// </summary>
    ///
    /// <param name="button">
    /// Specifies a <see cref="MouseButton"/> that represents the button to check.
    /// </param>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if a key has been released since the previous iteration. This example assumes that an implementation of <see cref="IMouse"/> has been provided, such as <see cref="Mouse"/>.
    ///
    /// <code>
    /// var isReleased = mouse.IsButtonReleased(MouseButton.Middle);
    ///
    /// if (isPressed)
    /// {
    ///     Console.WriteLine("The middle button has been released.");
    /// }
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// <c>true</c> if the specified <paramref name="button"/> has been released since the previous iteration; otherwise, <c>false</c>.
    /// </returns>
    bool IsButtonReleased(MouseButton button);

    /// <summary>
    /// Updates this <see cref="IMouse"/>.
    /// </summary>
    ///
    /// <remarks>
    /// This method's implementation should only be called after the user has checked for input state changes.
    /// </remarks>
    void Update();
}
