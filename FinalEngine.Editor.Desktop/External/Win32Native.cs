// <copyright file="Win32Native.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.External;

using System.Runtime.InteropServices;

internal static partial class Win32Native
{
    [LibraryImport("user32.dll")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial void SetCursorPos(int x, int y);
}
