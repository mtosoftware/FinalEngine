// <copyright file="IEntitySystemsProcessor.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS
{
    public interface IEntitySystemsProcessor
    {
        void ProcessAll(GameLoopType type);
    }
}