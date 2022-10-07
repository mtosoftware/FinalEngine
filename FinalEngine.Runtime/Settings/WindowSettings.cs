// <copyright file="WindowSettings.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Settings
{
    using System.Drawing;

    public class WindowSettings
    {
        public WindowSettings()
        {
            this.Title = "Game";
            this.Size = new Size(1280, 720);
        }

        public Size Size { get; set; }

        public string Title { get; set; }
    }
}
