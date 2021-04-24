// <copyright file="TextureBinder.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;
    using System.Collections.Generic;
    using FinalEngine.Rendering.Textures;

    public class TextureBinder : ITextureBinder
    {
        private readonly IPipeline pipeline;

        private readonly IDictionary<ITexture, int> textureToSlotMap;

        public TextureBinder(IPipeline pipeline)
        {
            this.pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline), $"The specified {nameof(pipeline)} parameter cannot be null.");

            this.textureToSlotMap = new Dictionary<ITexture, int>();

            for (int i = 0; i < pipeline.MaxTextureSlots; i++)
            {
                pipeline.SetUniform($"u_textures[{i}]", i);
            }
        }

        public bool ShouldReset
        {
            get { return this.textureToSlotMap.Count >= this.pipeline.MaxTextureSlots; }
        }

        public int GetTextureSlotIndex(ITexture texture)
        {
            if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture), $"The specified {nameof(texture)} parameter cannot be null.");
            }

            if (this.textureToSlotMap.ContainsKey(texture))
            {
                return this.textureToSlotMap[texture];
            }

            int slot = this.textureToSlotMap.Count;

            this.pipeline.SetTexture(texture, slot);
            this.textureToSlotMap.Add(texture, slot);

            return slot;
        }

        public void Reset()
        {
            this.textureToSlotMap.Clear();
        }
    }
}