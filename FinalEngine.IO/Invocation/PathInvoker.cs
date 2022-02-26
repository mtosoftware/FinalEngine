// <copyright file="PathInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO.Invocation
{
    using System.Collections.Generic;
    using System.IO;

    public class PathInvoker : IPathInvoker
    {
        public IEnumerable<char> GetInvalidFileNameChars()
        {
            return Path.GetInvalidFileNameChars();
        }

        public IEnumerable<char> GetInvalidPathChars()
        {
            return Path.GetInvalidPathChars();
        }
    }
}