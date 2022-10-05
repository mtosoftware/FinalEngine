// <copyright file="IMesh.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System;

    public interface IMesh : IDisposable
    {
        void Bind(IInputAssembler inputAssembler);

        void Render(IRenderDevice renderDevice);
    }
}