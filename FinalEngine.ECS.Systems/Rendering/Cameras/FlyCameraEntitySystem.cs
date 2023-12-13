// <copyright file="FlyCameraEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Systems.Rendering.Cameras;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Numerics;
using FinalEngine.ECS.Components.Core;
using FinalEngine.ECS.Components.Rendering.Cameras;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using FinalEngine.Maths;

[EntitySystemProcess(ExecutionType = GameLoopType.Update)]
public sealed class FlyCameraEntitySystem : EntitySystemBase
{
    private readonly IKeyboard keyboard;

    private readonly IMouse mouse;

    public FlyCameraEntitySystem(IKeyboard keyboard, IMouse mouse)
    {
        this.keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
        this.mouse = mouse ?? throw new ArgumentNullException(nameof(mouse));
    }

    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>() &&
               entity.ContainsComponent<VelocityComponent>() &&
               entity.ContainsComponent<PerspectiveCameraComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var velocity = entity.GetComponent<VelocityComponent>();
            var camera = entity.GetComponent<PerspectiveCameraComponent>();

            if (!camera.IsEnabled)
            {
                continue;
            }

            this.HandleKeyboard(transform, velocity, camera);
            this.HandleMouse(transform, velocity, camera);
        }
    }

    private void HandleKeyboard(TransformComponent transform, VelocityComponent velocity, PerspectiveCameraComponent camera)
    {
        float moveAmount = velocity.Speed;

        if (this.keyboard.IsKeyDown(Key.W))
        {
            transform.Translate(transform.Forward, moveAmount);
        }

        if (this.keyboard.IsKeyDown(Key.S))
        {
            transform.Translate(transform.Forward, -moveAmount);
        }

        if (this.keyboard.IsKeyDown(Key.A))
        {
            transform.Translate(transform.Left, -moveAmount);
        }

        if (this.keyboard.IsKeyDown(Key.D))
        {
            transform.Translate(transform.Left, moveAmount);
        }

        if (this.keyboard.IsKeyReleased(Key.Escape))
        {
            camera.IsLocked = false;
        }
    }

    private void HandleMouse(TransformComponent transform, VelocityComponent velocity, PerspectiveCameraComponent camera)
    {
        var viewport = camera.Viewport;
        var centerPosition = new Vector2(viewport.Width / 2, viewport.Height / 2);

        if (this.mouse.IsButtonReleased(MouseButton.Right))
        {
            this.mouse.Location = new PointF(centerPosition.X, centerPosition.Y);

            camera.IsLocked = true;
        }

        if (camera.IsLocked)
        {
            var deltaPosition = new Vector2(this.mouse.Location.X - centerPosition.X, this.mouse.Location.Y - centerPosition.Y);

            bool canRotateX = deltaPosition.X != 0;
            bool canRotateY = deltaPosition.Y != 0;

            if (canRotateX)
            {
                transform.Rotate(transform.Left, -MathHelper.DegreesToRadians(deltaPosition.Y * velocity.Speed));
            }

            if (canRotateY)
            {
                transform.Rotate(Vector3.UnitY, -MathHelper.DegreesToRadians(deltaPosition.X * velocity.Speed));
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
