// <copyright file="MouseButton.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System.Diagnostics.CodeAnalysis;

/// <summary>
///   Enumerates the available mouse buttons on a mouse.
/// </summary>
/// <remarks>
///   The values of each enumeration corresponds to OpenTK's MouseButtons enumeration.
/// </remarks>
[SuppressMessage("Design", "CA1027:Mark enums with FlagsAttribute", Justification = "Not required by API")]
public enum MouseButton
{
    /// <summary>
    ///   The first button.
    /// </summary>
    Button1 = 0,

    /// <summary>
    ///   The left mouse button. This corresponds to <see cref="Button1"/>.
    /// </summary>
    Left = Button1,

    /// <summary>
    ///   The second button.
    /// </summary>
    Button2 = 1,

    /// <summary>
    ///   The right mouse button. This corresponds to <see cref="Button2"/>.
    /// </summary>
    Right = Button2,

    /// <summary>
    ///   The third button.
    /// </summary>
    Button3 = 2,

    /// <summary>
    ///   The middle mouse button. This corresponds to <see cref="Button3"/>.
    /// </summary>
    Middle = Button3,

    /// <summary>
    ///   The fourth button.
    /// </summary>
    Button4 = 3,

    /// <summary>
    ///   The fifth button.
    /// </summary>
    Button5 = 4,

    /// <summary>
    ///   The sixth button.
    /// </summary>
    Button6 = 5,

    /// <summary>
    ///   The seventh button.
    /// </summary>
    Button7 = 6,

    /// <summary>
    ///   The eighth button.
    /// </summary>
    Button8 = 7,

    /// <summary>
    ///   The highest mouse button available.
    /// </summary>
    Last = Button8,
}
