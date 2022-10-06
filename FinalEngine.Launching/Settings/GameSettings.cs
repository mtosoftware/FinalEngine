// <copyright file="GameSettings.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Settings
{
    public class GameSettings
    {
        public GameSettings()
        {
            this.FrameCap = 120.0d;
            this.WindowSettings = new WindowSettings();
        }

        public double FrameCap { get; set; }

        public WindowSettings WindowSettings { get; set; }
    }
}