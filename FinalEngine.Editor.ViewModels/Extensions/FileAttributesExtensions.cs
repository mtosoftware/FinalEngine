// <copyright file="FileAttributesExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Extensions
{
    using System.IO;

    /// <summary>
    ///   Provides extension methods to the <see cref="FileAttributes"/> class.
    /// </summary>
    public static class FileAttributesExtensions
    {
        /// <summary>
        ///   Determines whether this instance contains flags that should be included in a file system search.
        /// </summary>
        /// <param name="fileAttributes">
        ///   The file attributes.
        /// </param>
        /// <returns>
        ///   <c>true</c> if this instance contains flags that be included in a file system search; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanIncludeInSearch(this FileAttributes fileAttributes)
        {
            return !(fileAttributes.HasFlag(FileAttributes.Hidden) || fileAttributes.HasFlag(FileAttributes.System) || fileAttributes.HasFlag(FileAttributes.Temporary));
        }
    }
}