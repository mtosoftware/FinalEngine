// <copyright file="Project.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Models;

public sealed class Project
{
    public Project(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new System.ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }

        this.Name = name;
    }

    public string Name { get; }
}
