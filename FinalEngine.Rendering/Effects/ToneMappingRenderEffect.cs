// <copyright file="HDRRenderEffect.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Effects;

using System;

public enum ToneMappingAlgorithm
{
    Reinhard,

    Exposure,
}

public sealed class ToneMappingRenderEffect : IRenderEffect
{
    public ToneMappingRenderEffect()
    {
        this.Enabled = true;
        this.Algorithm = ToneMappingAlgorithm.Exposure;
        this.Exposure = 1.0f;
    }

    public ToneMappingAlgorithm Algorithm { get; set; }

    public bool Enabled { get; set; }

    public float Exposure { get; set; }

    public void Bind(IPipeline pipeline)
    {
        ArgumentNullException.ThrowIfNull(pipeline, nameof(pipeline));

        pipeline.SetUniform("u_hdr.enabled", this.Enabled);
        pipeline.SetUniform("u_hdr.type", (int)this.Algorithm);

        if (this.Algorithm == ToneMappingAlgorithm.Exposure)
        {
            pipeline.SetUniform("u_hdr.exposure", this.Exposure);
        }
    }
}
