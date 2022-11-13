// <copyright file="RenderQualitySettings.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    public enum AntiAliasing
    {
        None,

        TwoTimesMultiSampling,

        FourTimesMultiSampling,

        EightTimesMultiSampling,
    }

    public struct RenderQualitySettings
    {
        private AntiAliasing? antiAliasing;

        private bool? multiSamplingEnabled;

        public AntiAliasing AntiAliasing
        {
            get { return this.antiAliasing ?? AntiAliasing.FourTimesMultiSampling; }
            set { this.antiAliasing = value; }
        }

        public int AntiAliasingSamples
        {
            get
            {
                return this.AntiAliasing switch
                {
                    AntiAliasing.None => 0,
                    AntiAliasing.TwoTimesMultiSampling => 2,
                    AntiAliasing.FourTimesMultiSampling => 4,
                    AntiAliasing.EightTimesMultiSampling => 8,
                    _ => 4,
                };
            }
        }

        public bool MultiSamplingEnabled
        {
            get { return this.multiSamplingEnabled ?? true; }
            set { this.multiSamplingEnabled = value; }
        }
    }
}
