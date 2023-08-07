// <copyright file="LoadWindowLayoutMessage.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Messages.Layout;

public sealed class LoadWindowLayoutMessage
{
    public LoadWindowLayoutMessage(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new System.ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));
        }

        this.FilePath = filePath;
    }

    public string FilePath { get; }
}
