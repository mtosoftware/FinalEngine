// <copyright file="IPresenterFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Presenters
{
    using System;
    using FinalEngine.Editor.Views;

    /// <summary>
    ///   Defines an interface that provides methods for creating presenters from a view.
    /// </summary>
    public interface IPresenterFactory
    {
        /// <summary>
        ///   Creates a <see cref="MainPresenter"/> from the specified <paramref name="view"/>.
        /// </summary>
        /// <param name="view">
        ///   The view used to create the presetner.
        /// </param>
        /// <returns>
        ///   The newly created <see cref="MainPresenter"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="view"/> parameter cannot be null.
        /// </exception>
        MainPresenter CreateMainPresenter(IMainView view);
    }
}
