// <copyright file="ISaveWindowLayoutViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Dialogs.Layout;

using CommunityToolkit.Mvvm.Input;

public interface ISaveWindowLayoutViewModel
{
    string LayoutName { get; set; }

    IRelayCommand SaveCommand { get; }

    string Title { get; }
}
