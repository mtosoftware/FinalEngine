namespace FinalEngine.Examples.Sponza;

using System;
using System.Drawing;
using System.Numerics;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Maths;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Vapor.Core;

public sealed class Camera
{
    private float height;

    private bool isLocked;

    private float speed = 0.4f;

    private float width;

    public Camera(int width, int height)
    {
        this.width = width;
        this.height = height;

        this.Transform = new Transform
        {
            Position = new Vector3(0, 10, 0)
        };
        this.Transform.Rotate(Vector3.UnitX, MathHelper.DegreesToRadians(45.0f));
        this.isLocked = true;
    }

    public Matrix4x4 Projection
    {
        get { return Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(70.0f), this.width / this.height, 0.1f, 1000.0f); }
    }

    public Transform Transform { get; }

    public void Update(IPipeline pipeline, IKeyboard keyboard, IMouse mouse)
    {
        float moveAmount = speed;

        if (keyboard.IsKeyDown(Key.W))
        {
            Transform.Translate(Transform.Forward, moveAmount);
        }

        if (keyboard.IsKeyDown(Key.S))
        {
            Transform.Translate(Transform.Forward, -moveAmount);
        }

        if (keyboard.IsKeyDown(Key.A))
        {
            Transform.Translate(Transform.Left, -moveAmount);
        }

        if (keyboard.IsKeyDown(Key.D))
        {
            Transform.Translate(Transform.Left, moveAmount);
        }

        if (keyboard.IsKeyReleased(Key.Escape))
        {
            Console.WriteLine(this.Transform.Position);
            isLocked = false;
        }

        var viewport = new Rectangle(0, 0, (int)width, (int)height);
        var centerPosition = new Vector2(viewport.Width / 2, viewport.Height / 2);

        if (mouse.IsButtonReleased(MouseButton.Right))
        {
            mouse.Location = new PointF(centerPosition.X, centerPosition.Y);

            isLocked = true;
        }

        if (isLocked)
        {
            var deltaPosition = new Vector2(mouse.Location.X - centerPosition.X, mouse.Location.Y - centerPosition.Y);

            bool canRotateX = deltaPosition.X != 0;
            bool canRotateY = deltaPosition.Y != 0;

            if (canRotateX)
            {
                Transform.Rotate(Transform.Left, -MathHelper.DegreesToRadians(deltaPosition.Y * speed));
            }

            if (canRotateY)
            {
                Transform.Rotate(Vector3.UnitY, -MathHelper.DegreesToRadians(deltaPosition.X * speed));
            }

            if (canRotateX || canRotateY)
            {
                mouse.Location = new PointF(
                    centerPosition.X,
                    centerPosition.Y);
            }
        }

        pipeline.SetUniform("u_viewPosition", Transform.Position);
        pipeline.SetUniform("u_projection", Projection);
        pipeline.SetUniform("u_view", Transform.CreateViewMatrix(Vector3.UnitY));
    }
}
