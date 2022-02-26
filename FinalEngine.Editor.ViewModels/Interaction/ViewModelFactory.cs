// <copyright file="ViewModelFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Interaction
{
    using System;
    using FinalEngine.Editor.Common.Services;

    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IProjectFileHandler projectFileHandler;

        private readonly IUserActionRequester userActionRequester;

        public ViewModelFactory(IUserActionRequester userActionRequester, IProjectFileHandler projectFileHandler)
        {
            this.userActionRequester = userActionRequester ?? throw new ArgumentNullException(nameof(userActionRequester));
            this.projectFileHandler = projectFileHandler ?? throw new ArgumentNullException(nameof(projectFileHandler));
        }

        public INewProjectViewModel CreateNewProjectViewModel()
        {
            return new NewProjectViewModel(this.userActionRequester, this.projectFileHandler);
        }
    }
}