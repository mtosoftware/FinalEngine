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

    private Transform transform;

    private float width;

    public Camera(int width, int height)
    {
        this.width = width;
        this.height = height;

        this.transform = new Transform();
        this.transform.Position = new Vector3(0, 10, 0);
        this.transform.Rotate(Vector3.UnitX, MathHelper.DegreesToRadians(45.0f));
        this.isLocked = true;
    }

    public Matrix4x4 Projection
    {
        get { return Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(70.0f), this.width / this.height, 0.1f, 1000.0f); }
    }

    public void Update(IPipeline pipeline, IKeyboard keyboard, IMouse mouse)
    {
        float moveAmount = speed;

        if (keyboard.IsKeyDown(Key.W))
        {
            transform.Translate(transform.Forward, moveAmount);
        }

        if (keyboard.IsKeyDown(Key.S))
        {
            transform.Translate(transform.Forward, -moveAmount);
        }

        if (keyboard.IsKeyDown(Key.A))
        {
            transform.Translate(transform.Left, -moveAmount);
        }

        if (keyboard.IsKeyDown(Key.D))
        {
            transform.Translate(transform.Left, moveAmount);
        }

        if (keyboard.IsKeyReleased(Key.Escape))
        {
            Console.WriteLine(this.transform.Position);
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
                transform.Rotate(transform.Left, -MathHelper.DegreesToRadians(deltaPosition.Y * speed));
            }

            if (canRotateY)
            {
                transform.Rotate(Vector3.UnitY, -MathHelper.DegreesToRadians(deltaPosition.X * speed));
            }

            if (canRotateX || canRotateY)
            {
                mouse.Location = new PointF(
                    centerPosition.X,
                    centerPosition.Y);
            }
        }

        pipeline.SetUniform("u_viewPosition", transform.Position);
        pipeline.SetUniform("u_projection", Projection);
        pipeline.SetUniform("u_view", transform.CreateViewMatrix(Vector3.UnitY));
    }
}
