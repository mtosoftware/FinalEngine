// <copyright file="FreeRotationEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Systems.Input
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Numerics;
    using FinalEngine.ECS.Components.Core;
    using FinalEngine.Input;
    using FinalEngine.Maths;
    using FinalEngine.Rendering;

    public class FreeRotationEntitySystem : EntitySystemBase
    {
        private readonly IKeyboard keyboard;

        private readonly IMouse mouse;

        private readonly IRasterizer rasterizer;

        private bool isLocked;

        public FreeRotationEntitySystem(IKeyboard keyboard, IMouse mouse, IRasterizer rasterizer)
        {
            this.keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
            this.mouse = mouse ?? throw new ArgumentNullException(nameof(mouse));
            this.rasterizer = rasterizer ?? throw new ArgumentNullException(nameof(rasterizer));

            this.LoopType = GameLoopType.Update;
        }

        public override GameLoopType LoopType { get; }

        protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
        {
            return entity.ContainsComponent<TransformComponent>() &&
                   entity.ContainsComponent<VelocityComponent>();
        }

        protected override void Process([NotNull] IEnumerable<Entity> entities)
        {
            Rectangle viewport = this.rasterizer.GetViewport();

            foreach (dynamic entity in entities)
            {
                TransformComponent transform = entity.Transform;
                VelocityComponent velocity = entity.Velocity;

                var centerPosition = new Vector2(viewport.Width / 2, viewport.Height / 2);

                if (this.keyboard.IsKeyReleased(Key.Escape))
                {
                    this.isLocked = false;
                }

                if (this.mouse.IsButtonReleased(MouseButton.Left))
                {
                    this.mouse.Location = new PointF(
                        centerPosition.X,
                        centerPosition.Y);

                    this.isLocked = true;
                }

                if (this.isLocked)
                {
                    var deltaPosition = new Vector2(
                        this.mouse.Location.X - centerPosition.X,
                        this.mouse.Location.Y - centerPosition.Y);

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
    }
}