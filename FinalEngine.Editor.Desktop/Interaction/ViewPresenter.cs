// <copyright file="ViewPresenter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Interaction
{
    using System;
    using FinalEngine.Editor.Desktop.Views;
    using FinalEngine.Editor.ViewModels;
    using FinalEngine.Editor.ViewModels.Interaction;

    public class ViewPresenter : IViewPresenter
    {
        public void ShowNewProjectView(INewProjectViewModel newProjectViewModel)
        {
            if (newProjectViewModel == null)
            {
                throw new ArgumentNullException(nameof(newProjectViewModel));
            }

            var view = new NewProjectView()
            {
                DataContext = newProjectViewModel,
            };

            view.ShowDialog();
        }
    }
}