// <copyright file="ICreateNewEntityViewModel.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Entities;

using System.Windows.Input;

public interface ICreateNewEntityViewModel
{
    ICommand CreateCommand { get; }

    string EntityName { get; }
}
