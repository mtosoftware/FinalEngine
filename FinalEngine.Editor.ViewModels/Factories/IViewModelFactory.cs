// <copyright file="IViewModelFactory.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Factories;

public interface IViewModelFactory
{
    TViewModel CreateViewModel<TViewModel>()
        where TViewModel : IViewModel;
}
