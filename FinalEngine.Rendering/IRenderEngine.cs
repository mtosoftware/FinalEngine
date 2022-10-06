// <copyright file="IRenderEngine.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using FinalEngine.ECS;

    public interface IRenderEngine
    {
        void Initialize();

        void Render(IEntitySystemsProcessor processor);
    }
}