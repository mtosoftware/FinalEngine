// <copyright file="TempClass.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace MyInputExample;

using System;
using FinalEngine.Input.Keyboards;

public class InputExample
{
    private readonly Keyboard keyboard;

    public InputExample(Keyboard keyboard)
    {
        // Generally speaking you should inject the IKeyboard interface.
        this.keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
    }

    public void HandleInput()
    {
        // Check if a key is being held down.
        if (this.keyboard.IsKeyDown(Key.A))
        {
            Console.WriteLine("The A key is being held down.");
        }

        // Check if a key has been pressed.
        if (this.keyboard.IsKeyPressed(Key.B))
        {
            Console.WriteLine("The B key has been pressed.");
        }

        // Check if a key has been released.
        if (this.keyboard.IsKeyReleased(Key.Space))
        {
            Console.WriteLine("The space bar key has been released.");
        }

        // Also check for lockable keys.
        if (this.keyboard.IsCapsLocked && this.keyboard.IsKeyReleased(Key.F))
        {
            Console.WriteLine("Wow, the caps lock key is locked AND you pressed the F key!");
        }

        // Lastly, there is also modifier keys that can be checked via properties as well.
        if (this.keyboard.IsAltDown && this.keyboard.IsKeyPressed(Key.F4))
        {
            Environment.Exit(0);
        }

        // If you're using a GameContainerBase implementation you won't need to invoke this method.
        // But, if you aren't you will need to invoke the method after handling input state changes.
        this.keyboard.Update();
    }
}
