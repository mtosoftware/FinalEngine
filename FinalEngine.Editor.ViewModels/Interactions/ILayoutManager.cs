// <copyright file="ILayoutManager.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interactions;

using System.Collections.Generic;

public interface ILayoutManager
{
    bool ContainsLayout(string layoutName);

    void DeleteLayout(string layoutName);

    void LoadLayout(string layoutName);

    IEnumerable<string> LoadLayoutNames();

    void ResetLayout();

    void SaveLayout(string layoutName);

    void ToggleToolWindow(string contentID);
}
