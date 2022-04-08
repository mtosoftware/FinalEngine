// <copyright file="FileAttributesExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Extensions
{
    using System.IO;

    public static class FileAttributesExtensions
    {
        public static bool CanIncludeInSearch(this FileAttributes fileAttributes)
        {
            return !(fileAttributes.HasFlag(FileAttributes.Hidden) || fileAttributes.HasFlag(FileAttributes.System) || fileAttributes.HasFlag(FileAttributes.Temporary));
        }
    }
}