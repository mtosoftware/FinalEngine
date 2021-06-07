// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    using System;
    using FinalEngine.ECS;

    public class CoolComponent : IComponent
    {
        public CoolComponent(string text)
        {
            this.Text = text;
        }

        public string Text { get; }
    }

    internal static class Program
    {
        private static void Main()
        {
            dynamic entity = new Entity();
            entity.AddComponent(new CoolComponent("Testing This!"));

            CoolComponent cool = entity.Cool;

            Console.WriteLine(cool.Text);
        }
    }
}