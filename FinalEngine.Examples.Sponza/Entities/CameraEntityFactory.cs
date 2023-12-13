namespace FinalEngine.Examples.Sponza.Entities;

using System.Drawing;
using FinalEngine.ECS;
using FinalEngine.ECS.Components.Core;
using FinalEngine.ECS.Components.Rendering.Cameras;

public sealed class CameraEntityFactory : IEntityFactory
{
    private readonly float height;

    private readonly float width;

    public CameraEntityFactory(float width, float height)
    {
        this.width = width;
        this.height = height;
    }

    public Entity CreateEntity()
    {
        var entity = new Entity();

        entity.AddComponent<TransformComponent>();

        entity.AddComponent(new PerspectiveCameraComponent()
        {
            AspectRatio = this.width / this.height,
            FieldOfView = 70.0f,
            NearPlaneDistance = 0.1f,
            FarPlaneDistance = 1000.0f,
            IsEnabled = true,
            Viewport = new Rectangle(0, 0, (int)this.width, (int)this.height),
        });

        entity.AddComponent(new VelocityComponent()
        {
            Speed = 0.4f,
        });

        return entity;
    }
}
