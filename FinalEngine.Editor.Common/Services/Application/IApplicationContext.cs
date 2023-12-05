// <copyright file="IApplicationContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Application;

using System;

public interface IApplicationContext
{
    string DataDirectory { get; }

    string Title { get; }

    Version Version { get; }
}
