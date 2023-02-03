// <copyright file="KeyModifiers.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Keyboards;

using System;

/// <summary>
///   Enumerates the available key modifiers, such as Shift or CTRL.
/// </summary>
/// <remarks>
///   The values of each enumeration corresponds to OpenTK's KeyModifiers enumeration.
/// </remarks>
[Flags]
public enum KeyModifiers
{
    /// <summary>
    ///   Specifies no modifiers were held down.
    /// </summary>
    None = 0,

    /// <summary>
    ///   Specifies if one or more Shift keys were held down.
    /// </summary>
    Shift = 0x0001,

    /// <summary>
    ///   Specifies if one or more Control keys were held down.
    /// </summary>
    Control = 0x0002,

    /// <summary>
    ///   Specifies if one or more Alt keys were held down.
    /// </summary>
    Alt = 0x0004,

    /// <summary>
    ///   Specifies if one or more Super keys were held down.
    /// </summary>
    Super = 0x0008,

    /// <summary>
    ///   Specifies if caps lock is enabled.
    /// </summary>
    CapsLock = 0x0010,

    /// <summary>
    ///   Specifies if num lock is enabled.
    /// </summary>
    NumLock = 0x0020,
}
