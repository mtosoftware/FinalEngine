// <copyright file="IKeyboard.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Keyboards;

/// <summary>
///   Defines an interface that provides real time handling of keyboard operations.
/// </summary>
public interface IKeyboard
{
    /// <summary>
    ///   Gets a value indicating whether the <see cref="Key.LeftAlt"/> or <see cref="Key.RightAlt"/> is down during the current frame.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the <see cref="Key.LeftAlt"/> or <see cref="Key.RightAlt"/> is down during the current frame; otherwise, <c>false</c>.
    /// </value>
    bool IsAltDown { get; }

    /// <summary>
    ///   Gets a value indicating whether the <see cref="Key.CapsLock"/> is currently locked.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the <see cref="Key.CapsLock"/> is currently locked; otherwise, <c>false</c>.
    /// </value>
    bool IsCapsLocked { get; }

    /// <summary>
    ///   Gets a value indicating whether the <see cref="Key.LeftControl"/> or <see cref="Key.RightControl"/> is down during the current frame.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the <see cref="Key.LeftControl"/> or <see cref="Key.RightControl"/> is down during the current frame; otherwise, <c>false</c>.
    /// </value>
    bool IsControlDown { get; }

    /// <summary>
    ///   Gets a value indicating whether the <see cref="Key.NumLock"/> is currently locked.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the <see cref="Key.NumLock"/> is currently locked; otherwise, <c>false</c>.
    /// </value>
    bool IsNumLocked { get; }

    /// <summary>
    ///   Gets a value indicating whether the <see cref="Key.LeftShift"/> or <see cref="Key.RightShift"/> is down during the current frame.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the <see cref="Key.LeftShift"/> or <see cref="Key.RightShift"/> is down during the current frame; otherwise, <c>false</c>.
    /// </value>
    bool IsShiftDown { get; }

    /// <summary>
    ///   Determines whether the specified <paramref name="key"/> is down during the current frame.
    /// </summary>
    /// <param name="key">
    ///   Specifies a <see cref="Key"/> that represents the key to check for.
    /// </param>
    /// <returns>
    ///   <c>true</c> if the specified <paramref name="key"/> is down during the current frame; otherwise, <c>false</c>.
    /// </returns>
    bool IsKeyDown(Key key);

    /// <summary>
    ///   Determines whether the specified <paramref name="key"/> has been pressed this frame.
    /// </summary>
    /// <param name="key">
    ///   Specifies a <see cref="Key"/> that represents the key to check for.
    /// </param>
    /// <returns>
    ///   <c>true</c> if the specified <paramref name="key"/> has been pressed this frame; otherwise, <c>false</c>.
    /// </returns>
    bool IsKeyPressed(Key key);

    /// <summary>
    ///   Determines whether the specified <paramref name="key"/> has been released since the previous frame.
    /// </summary>
    /// <param name="key">
    ///   Specifies a <see cref="Key"/> that represents the key to check for.
    /// </param>
    /// <returns>
    ///   <c>true</c> if the specified <paramref name="key"/> has been released since the previous frame; otherwise, <c>false</c>.
    /// </returns>
    bool IsKeyReleased(Key key);

    /// <summary>
    ///   Updates this <see cref="IKeyboard"/>.
    /// </summary>
    void Update();
}
