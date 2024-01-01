// <copyright file="EntityComponentTypeResolver.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Services.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using FinalEngine.ECS;

public sealed class EntityComponentTypeResolver : IEntityComponentTypeResolver
{
    public IReadOnlyDictionary<string, List<Type>> GetCategorizedTypes(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));

        var categoryToTypeMap = new Dictionary<string, List<Type>>();

        var componentTypes = assembly.GetTypes()
            .Where(x =>
            {
                return typeof(IEntityComponent).IsAssignableFrom(x) && x.GetConstructor(Type.EmptyTypes) != null;
            });

        foreach (var componentType in componentTypes)
        {
            var categoryAttribute = componentType.GetCustomAttribute<CategoryAttribute>();
            string category = categoryAttribute?.Category ?? "Uncategorized";

            if (!categoryToTypeMap.TryGetValue(category, out var types))
            {
                types = [];
                categoryToTypeMap.Add(category, types);
            }

            types.Add(componentType);
        }

        return categoryToTypeMap;
    }
}
