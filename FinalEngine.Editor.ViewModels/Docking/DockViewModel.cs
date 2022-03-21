// <copyright file="DockViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking
{
    using System;
    using System.Collections.Generic;
    using FinalEngine.Editor.ViewModels.Interaction;

    public class DockViewModel : IDockViewModel
    {
        public DockViewModel(IViewModelFactory viewModelFactory)
        {
            if (viewModelFactory == null)
            {
                throw new ArgumentNullException(nameof(viewModelFactory));
            }

            this.Tools = new List<IToolViewModel>()
            {
                viewModelFactory.CreateProjectExplorerViewModel(),
            };

            this.Documents = new List<IPaneViewModel>();
        }

        public IEnumerable<IPaneViewModel> Documents { get; }

        public IEnumerable<IToolViewModel> Tools { get; }
    }
}