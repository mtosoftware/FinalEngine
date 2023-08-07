// <copyright file="ToggleToolWindowMessage.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Messages.Docking;

public sealed class ToggleToolWindowMessage
{
    public ToggleToolWindowMessage(string contentID)
    {
        if (string.IsNullOrWhiteSpace(contentID))
        {
            throw new System.ArgumentException($"'{nameof(contentID)}' cannot be null or whitespace.", nameof(contentID));
        }

        this.ContentID = contentID;
    }

    public string ContentID { get; }
}
