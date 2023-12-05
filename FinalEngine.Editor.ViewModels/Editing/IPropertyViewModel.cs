// <copyright file="IPropertyViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing;

public interface IPropertyViewModel<T>
{
    string Name { get; }

    T? Value { get; set; }
}
