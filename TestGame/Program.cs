// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    using FinalEngine.Launching;

    internal static class Program
    {
        private static void Main()
        {
            using (var game = new Game())
            {
                game.Launch(new GameTime(120.0d));
            }
        }
    }
}