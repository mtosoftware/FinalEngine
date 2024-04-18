// <copyright file="EditorCamera.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes.Cameras;

using System;
using System.Drawing;
using System.Numerics;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Maths;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Geometry;

public sealed class EditorCamera : ICamera
{
    private const float FarPlane = 1000.0f;

    private const float FieldOfView = 70.0f;

    private const float MouseSensitivity = 0.25f;

    private const float MoveSpeed = 0.5f;

    private const float NearPlane = 0.1f;

    private readonly IKeyboard keyboard;

    private readonly IMouse mouse;

    private bool isLocked;

    public EditorCamera(IKeyboard keyboard, IMouse mouse)
    {
        this.keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
        this.mouse = mouse ?? throw new ArgumentNullException(nameof(mouse));

        this.isLocked = false;

        this.Bounds = new Rectangle(0, 0, 1280, 720);

        this.Transform = new TransformComponent()
        {
            Position = new Vector3(0, 50, 0),
            Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(45.0f)),
        };
    }

    public Rectangle Bounds { get; set; }

    public Matrix4x4 Projection
    {
        get { return Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FieldOfView), this.AspectRatio, NearPlane, FarPlane); }
    }

    public TransformComponent Transform { get; }

    public Matrix4x4 View
    {
        get { return this.Transform.CreateViewMatrix(Vector3.UnitY); }
    }

    private int AspectRatio
    {
        get { return this.Bounds.Width / this.Bounds.Height; }
    }

    public void Update()
    {
        ArgumentNullException.ThrowIfNull(this.keyboard, nameof(this.keyboard));
        ArgumentNullException.ThrowIfNull(this.mouse, nameof(this.mouse));

        float moveAmount = MoveSpeed;

        if (this.keyboard.IsKeyDown(Key.W))
        {
            this.Transform.Translate(this.Transform.Forward, moveAmount);
        }

        if (this.keyboard.IsKeyDown(Key.S))
        {
            this.Transform.Translate(this.Transform.Forward, -moveAmount);
        }

        if (this.keyboard.IsKeyDown(Key.A))
        {
            this.Transform.Translate(this.Transform.Left, -moveAmount);
        }

        if (this.keyboard.IsKeyDown(Key.D))
        {
            this.Transform.Translate(this.Transform.Left, moveAmount);
        }

        if (this.keyboard.IsKeyDown(Key.Z))
        {
            this.Transform.Translate(this.Transform.Up, moveAmount);
        }

        if (this.keyboard.IsKeyDown(Key.X))
        {
            this.Transform.Translate(this.Transform.Down, moveAmount);
        }

        if (this.keyboard.IsKeyReleased(Key.Escape))
        {
            this.isLocked = false;
        }

        var centerPosition = new Vector2(this.Bounds.Width / 2, this.Bounds.Height / 2);

        if (this.mouse.IsButtonReleased(MouseButton.Right))
        {
            this.mouse.Location = new PointF(centerPosition.X, centerPosition.Y);

            this.isLocked = true;
        }

        if (this.isLocked)
        {
            var deltaPosition = new Vector2(this.mouse.Location.X - centerPosition.X, this.mouse.Location.Y - centerPosition.Y);

            bool canRotateX = deltaPosition.X != 0;
            bool canRotateY = deltaPosition.Y != 0;

            if (canRotateX)
            {
                this.Transform.Rotate(this.Transform.Left, -MathHelper.DegreesToRadians(deltaPosition.Y * MouseSensitivity));
            }

            if (canRotateY)
            {
                this.Transform.Rotate(Vector3.UnitY, -MathHelper.DegreesToRadians(deltaPosition.X * MouseSensitivity));
            }

            if (canRotateX || canRotateY)
            {
                this.mouse.Location = new PointF(
                    centerPosition.X,
                    centerPosition.Y);
            }
        }
    }
}
