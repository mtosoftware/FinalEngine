// <copyright file="DockViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking
{
    using System;
    using System.Collections.Generic;
    using FinalEngine.Editor.ViewModels.Interaction;

    /// <summary>
    ///   Provides a standard implementation of an <see cref="IDockViewModel"/>.
    /// </summary>
    /// <seealso cref="IDockViewModel"/>
    public class DockViewModel : IDockViewModel
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="DockViewModel"/> class.
        /// </summary>
        /// <param name="viewModelFactory">
        ///   The view model factory.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        ///   The specified <paramref name="viewModelFactory"/> parameter cannot be null.
        /// </exception>
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

        /// <summary>
        ///   Gets the documents attached to this <see cref="IDockViewModel"/>.
        /// </summary>
        /// <value>
        ///   The documents attached to this <see cref="IDockViewModel"/>.
        /// </value>
        public IEnumerable<IPaneViewModel> Documents { get; }

        /// <summary>
        ///   Gets the tool windows attached to this <see cref="IDockViewModel"/>.
        /// </summary>
        /// <value>
        ///   The tool windows attached to this <see cref="IDockViewModel"/>.
        /// </value>
        public IEnumerable<IToolViewModel> Tools { get; }
    }
}