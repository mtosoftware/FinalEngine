// <copyright file="IPathInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.IO.Invocation
{
    using System.Collections.Generic;

    public interface IPathInvoker
    {
        IEnumerable<char> GetInvalidFileNameChars();

        IEnumerable<char> GetInvalidPathChars();
    }
}