// <copyright file="IEnvironmentContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Environment;

using System;

public interface IEnvironmentContext
{
    string GetFolderPath(Environment.SpecialFolder folder);
}
