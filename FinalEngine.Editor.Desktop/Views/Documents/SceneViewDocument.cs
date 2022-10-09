// <copyright file="SceneViewDocument.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Documents
{
    using System;
    using DarkUI.Docking;
    using FinalEngine.Editor.Presenters;

    /// <summary>
    ///   Provides a scene view document used to render the currently loaded scene to the user.
    /// </summary>
    /// <seealso cref="DarkDocument"/>
    public partial class SceneViewDocument : DarkDocument
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref="SceneViewDocument"/> class.
        /// </summary>
        /// <param name="presenterFactory">
        ///   The presenter factory.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///   The specified <paramref name="presenterFactory"/> parameter cannot be null.
        /// </exception>
        public SceneViewDocument(IPresenterFactory presenterFactory)
        {
            if (presenterFactory == null)
            {
                throw new ArgumentNullException(nameof(presenterFactory));
            }

            this.InitializeComponent();
        }
    }
}
