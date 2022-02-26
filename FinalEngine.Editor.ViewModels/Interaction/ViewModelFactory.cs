// <copyright file="ViewModelFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction
{
    public class ViewModelFactory : IViewModelFactory
    {
        public INewProjectViewModel CreateNewProjectViewModel()
        {
            return new NewProjectViewModel();
        }
    }
}