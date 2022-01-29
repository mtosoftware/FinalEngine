// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    internal static class Program
    {
        private static void Main()
        {
            using (var game = new Game())
            {
                game.Launch(120);
            }
        }
    }
}