// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Examples.StarWarriors
{
    internal class Program
    {
        private static void Main()
        {
            using (var game = new Game())
            {
                game.Launch(120.0d);
            }
        }
    }
}