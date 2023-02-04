// <copyright file="TempClass.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace MyInputExample;

using System;
using FinalEngine.Input.Mouses;

public class InputExample
{
    private readonly Mouse mouse;

    public InputExample(Mouse mouse)
    {
        // Generally speaking you should inject the IMouse interface.
        this.mouse = mouse ?? throw new ArgumentNullException(nameof(mouse));
    }

    public void HandleInput()
    {
        // Check if a button is being held down.
        if (this.mouse.IsButtonDown(MouseButton.Left))
        {
            Console.WriteLine("The left mouse button is being held down.");
        }

        // Check if a mouse button has been pressed.
        if (this.mouse.IsButtonPressed(MouseButton.Right))
        {
            Console.WriteLine("The right mouse button was pressed.");
        }

        // Check if a mouse button has been released.
        if (this.mouse.IsButtonReleased(MouseButton.Right))
        {
            Console.WriteLine("The right mouse button was released.");
        }

        // Get the change in position of the mouse since the previous frame, good for implementing FPS cameras.
        var delta = this.mouse.Delta;

        // Get the current position of the mouse.
        var position = this.mouse.Location;

        // Lastly, you can get the scroll wheel offset, basically how much it's moved in the Y-direction.
        double offset = this.mouse.WheelOffset;

        // If we moved the mouse wheel and the X positon of the mouse is greater than 100 window pixels AND we haven't moved the mouse this frame.
        if (offset > 1 && position.X > 100 && delta.IsEmpty)
        {
            Console.WriteLine("Wow, you really moved the scroll wheel without moving the mouse? Steady hands!");
        }

        // If you're using a GameContainerBase implementation you won't need to invoke this method.
        // But, if you aren't you will need to invoke the method after handling input state changes.
        this.mouse.Update();
    }
}
