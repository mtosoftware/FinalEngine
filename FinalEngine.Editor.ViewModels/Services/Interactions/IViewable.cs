// <copyright file="IViewable.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Services.Interactions;

public interface IViewable<TViewModel>
{
    object DataContext { get; set; }

    bool? ShowDialog();
}
