// <copyright file="IDockViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking
{
    using System.Collections.Generic;
    using FinalEngine.Editor.ViewModels.Docking.Panes;
    using FinalEngine.Editor.ViewModels.Docking.Tools;

    /// <summary>
    ///   Defines an interface that represents a dockable view model.
    /// </summary>
    public interface IDockViewModel
    {
        /// <summary>
        ///   Gets the documents attached to this <see cref="IDockViewModel"/>.
        /// </summary>
        /// <value>
        ///   The documents attached to this <see cref="IDockViewModel"/>.
        /// </value>
        IEnumerable<IPaneViewModel> Documents { get; }

        /// <summary>
        ///   Gets the tool windows attached to this <see cref="IDockViewModel"/>.
        /// </summary>
        /// <value>
        ///   The tool windows attached to this <see cref="IDockViewModel"/>.
        /// </value>
        IEnumerable<IToolViewModel> Tools { get; }
    }
}