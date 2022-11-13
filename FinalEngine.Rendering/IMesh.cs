// <copyright file="IMesh.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using FinalEngine.Resources;

    public interface IMesh : IResource
    {
        void Bind(IInputAssembler inputAssembler);

        void Render(IRenderDevice renderDevice);
    }
}
