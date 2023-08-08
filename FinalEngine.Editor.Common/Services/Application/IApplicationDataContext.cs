// <copyright file="IApplicationDataContext.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Application;

using System.Collections.Generic;

public interface IApplicationDataContext
{
    bool ContainsLayout(string layoutName);

    string GetLayoutPath(string layoutName);

    IEnumerable<string> LoadLayoutNames();
}
