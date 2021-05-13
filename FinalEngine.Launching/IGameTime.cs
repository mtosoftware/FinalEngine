// <copyright file="IGameTime.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Launching
{
    public interface IGameTime
    {
        bool CanProcessNextFrame(out GameTimeInfo info);
    }
}