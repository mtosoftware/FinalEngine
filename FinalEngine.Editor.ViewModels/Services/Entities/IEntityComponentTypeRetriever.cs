// <copyright file="IEntityComponentTypeRetriever.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Services.Entities;

using System;
using System.Collections.Generic;
using System.Reflection;

public interface IEntityComponentTypeRetriever
{
    IReadOnlyDictionary<string, List<Type>> GetCategorizedTypes(Assembly assembly);
}
