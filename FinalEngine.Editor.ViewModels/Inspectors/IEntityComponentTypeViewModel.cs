// <copyright file="IEntityComponentTypeViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Inspectors;

using System.Windows.Input;

public interface IEntityComponentTypeViewModel
{
    ICommand AddCommand { get; }

    string Name { get; }
}
