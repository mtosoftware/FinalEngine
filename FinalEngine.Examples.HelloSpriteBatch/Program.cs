﻿namespace FinalEngine.Examples.HelloSpriteBatch
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Launch(120.0d);
            }
        }
    }
}