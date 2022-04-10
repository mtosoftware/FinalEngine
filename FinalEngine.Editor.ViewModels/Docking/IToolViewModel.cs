// <copyright file="IToolViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking
{
    /// <summary>
    ///   Defines an interface that represents a tool window view model.
    /// </summary>
    /// <remarks>
    ///   A tool view is a view which is used as part of a dockable layout system. It represents any element that can be docked to the tool section of a dockable layout.
    /// </remarks>
    /// <seealso cref="FinalEngine.Editor.ViewModels.Docking.IPaneViewModel"/>
    public interface IToolViewModel : IPaneViewModel
    {
        /// <summary>
        ///   Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        bool IsVisible { get; set; }
    }
}