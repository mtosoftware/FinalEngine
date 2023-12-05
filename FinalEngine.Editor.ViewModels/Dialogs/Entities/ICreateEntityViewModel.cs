// <copyright file="ICreateEntityViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Entities;

using System;
using CommunityToolkit.Mvvm.Input;

public interface ICreateEntityViewModel
{
    IRelayCommand CreateCommand { get; }

    Guid EntityGuid { get; }

    string EntityName { get; set; }

    string Title { get; }
}
