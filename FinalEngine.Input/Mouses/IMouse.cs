// <copyright file="IMouse.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System.Drawing;

public interface IMouse
{
    PointF Delta { get; }

    PointF Location { get; set; }

    double WheelOffset { get; }

    bool IsButtonDown(MouseButton button);

    bool IsButtonPressed(MouseButton button);

    bool IsButtonReleased(MouseButton button);

    void Update();
}
