// <copyright file="IKeyboard.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Keyboards;

/// <summary>
/// Represents an interface that defines a keyboard that allows interaction with keyboard state and key events in real time.
/// </summary>
///
/// <remarks>
/// The <see cref="IKeyboard"/> interface provides properties and methods to query the current state of keys on the keyboard, as well as to check for key press and release events.
/// </remarks>
public interface IKeyboard
{
    /// <summary>
    /// Gets a value indicating whether the <see cref="Key.LeftAlt"/> or <see cref="Key.RightAlt"/> key is down during the current iteration.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the <see cref="Key.LeftAlt"/> or <see cref="Key.RightAlt"/> key is down during the current iteration; otherwise, <c>false</c>.
    /// </value>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if the <see cref="Key.LeftAlt"/> or <see cref="Key.RightAlt"/> key is down during the current iteration. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// bool isDown = keyboard.IsAltDown;
    ///
    /// while (isDown)
    /// {
    ///     Console.WriteLine("The left or right ALT key is currently held down.");
    /// }
    /// </code>
    /// </example>
    bool IsAltDown { get; }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Key.CapsLock"/> key is currently locked.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the <see cref="Key.CapsLock"/> key is currently locked; otherwise, <c>false</c>.
    /// </value>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if the CAPS LOCK key is currently in a locked state. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// bool isLocked = keyboard.IsCapsLocked;
    ///
    /// if (isLocked)
    /// {
    ///     Console.WriteLine("The CAPS LOCK key is currently in a locked state.");
    /// }
    /// </code>
    /// </example>
    bool IsCapsLocked { get; }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Key.LeftControl"/> or <see cref="Key.RightControl"/> key is down during the current iteration.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the <see cref="Key.LeftControl"/> or <see cref="Key.RightControl"/> key is down during the current iteration; otherwise, <c>false</c>.
    /// </value>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if the <see cref="Key.LeftControl"/> or <see cref="Key.RightControl"/> key is down during the current iteration. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// bool isDown = keyboard.IsControlDown;
    ///
    /// while (isDown)
    /// {
    ///     Console.WriteLine("The left or right CONTROL key is currently held down.");
    /// }
    /// </code>
    /// </example>
    bool IsControlDown { get; }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Key.NumLock"/> key is currently locked.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the <see cref="Key.NumLock"/> key is currently locked; otherwise, <c>false</c>.
    /// </value>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if the NUM LOCK key is currently in a locked state. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// bool isLocked = keyboard.IsNumLocked;
    ///
    /// if (isLocked)
    /// {
    ///     Console.WriteLine("The NUM LOCK key is currently in a locked state.");
    /// }
    /// </code>
    /// </example>
    bool IsNumLocked { get; }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Key.LeftShift"/> or <see cref="Key.RightShift"/> key is down during the current iteration.
    /// </summary>
    ///
    /// <value>
    /// <c>true</c> if the <see cref="Key.LeftShift"/> or <see cref="Key.RightShift"/> key is down during the current iteration; otherwise, <c>false</c>.
    /// </value>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if the <see cref="Key.LeftShift"/> or <see cref="Key.RightShift"/> key is down during the current iteration. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// bool isDown = keyboard.IsShiftDown;
    ///
    /// while (isDown)
    /// {
    ///     Console.WriteLine("The left or right SHIFT key is currently held down.");
    /// }
    /// </code>
    /// </example>
    bool IsShiftDown { get; }

    /// <summary>
    /// Determines whether the specified <paramref name="key"/> is down during the current iteration.
    /// </summary>
    ///
    /// <param name="key">
    /// Specifies a <see cref="Key"/> that represents the key to check.
    /// </param>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if a key is down during the current iteration. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// if (keyboard.IsKeyDown(Key.F))
    /// {
    ///     Console.WriteLine("The F key is currently held down.");
    /// }
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// Returns <c>true</c> if the specified <paramref name="key"/> is down during the current iteration; otherwise, <c>false</c>.
    /// </returns>
    bool IsKeyDown(Key key);

    /// <summary>
    /// Determines whether the specified <paramref name="key"/> has been pressed this iteration.
    /// </summary>
    ///
    /// <param name="key">
    /// Specifies a <see cref="Key"/> that represents the key to check.
    /// </param>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if a key has been pressed during the current iteration. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// if (keyboard.IsKeyPressed(Key.F))
    /// {
    ///     Console.WriteLine("The F key has been pressed.");
    /// }
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// Returns <c>true</c> if the specified <paramref name="key"/> has been pressed during this iteration; otherwise, <c>false</c>.
    /// </returns>
    bool IsKeyPressed(Key key);

    /// <summary>
    /// Determines whether the specified <paramref name="key"/> has been released since the previous iteration.
    /// </summary>
    ///
    /// <param name="key">
    /// Specifies a <see cref="Key"/> that represents the key to check.
    /// </param>
    ///
    /// <example>
    /// Below you'll find an example showing how to check if a key has been released since the previous iteration. This example assumes that an implementation of <see cref="IKeyboard"/> has been provided, such as <see cref="Keyboard"/>.
    ///
    /// <code>
    /// if (keyboard.IsKeyReleased(Key.F))
    /// {
    ///     Console.WriteLine("The F key has been released.");
    /// }
    /// </code>
    /// </example>
    ///
    /// <returns>
    /// Returns <c>true</c> if the specified <paramref name="key"/> has been released since the previous iteration; otherwise, <c>false</c>.
    /// </returns>
    bool IsKeyReleased(Key key);

    /// <summary>
    /// Updates this <see cref="IKeyboard"/>.
    /// </summary>
    ///
    /// <remarks>
    /// This method's implementation should only be called after the user has checked for input state changes.
    /// </remarks>
    void Update();
}
