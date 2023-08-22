// <copyright file="TransformComponent.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.ECS.Components.Core;

using System.Numerics;

/// <summary>
/// Provides a component that represents the translation, rotation and scale of an entity.
/// </summary>
/// <seealso cref="IEntityComponent" />
public sealed class TransformComponent : IEntityComponent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TransformComponent"/> class.
    /// </summary>
    public TransformComponent()
    {
        this.Position = Vector3.Zero;
        this.Rotation = Quaternion.Identity;
        this.Scale = Vector3.One;
    }

    /// <summary>
    /// Gets a normalized vector representing the negative Z-axis of the transform in world space.
    /// </summary>
    /// <value>
    /// The normalized vector representing the Z-axis of the transform in world space..
    /// </value>
    public Vector3 Backward
    {
        get { return Vector3.Normalize(Vector3.Transform(-Vector3.UnitZ, this.Rotation)); }
    }

    /// <summary>
    /// Gets a normalized vector representing the negative Y-axis of the transform in world space.
    /// </summary>
    /// <value>
    /// The normalized vector representing the negative Y-axis of the transform in world space.
    /// </value>
    public Vector3 Down
    {
        get { return Vector3.Normalize(Vector3.Transform(-Vector3.UnitY, this.Rotation)); }
    }

    /// <summary>
    /// Gets a normalized vector representing the Z-axis of the transform in world space.
    /// </summary>
    /// <value>
    /// The normalized vector representing the Z-axis of the transform in world space..
    /// </value>
    public Vector3 Forward
    {
        get { return Vector3.Normalize(Vector3.Transform(Vector3.UnitZ, this.Rotation)); }
    }

    /// <summary>
    /// Gets a normalized vector representing the negative X-axis of the transform in world space.
    /// </summary>
    /// <value>
    /// The normalized vector representing the negative X-axis of the transform in world space.
    /// </value>
    public Vector3 Left
    {
        get { return Vector3.Normalize(Vector3.Transform(-Vector3.UnitX, this.Rotation)); }
    }

    /// <summary>
    /// Gets or sets the position of the transform.
    /// </summary>
    /// <value>
    /// The position of the transform.
    /// </value>
    public Vector3 Position { get; set; }

    /// <summary>
    /// Gets a normalized vector representing the X-axis of the transform in world space.
    /// </summary>
    /// <value>
    /// The normalized vector representing the negative X-axis of the transform in world space.
    /// </value>
    public Vector3 Right
    {
        get { return Vector3.Normalize(Vector3.Transform(Vector3.UnitX, this.Rotation)); }
    }

    /// <summary>
    /// Gets or sets the rotation of this transform.
    /// </summary>
    /// <value>
    /// The rotation of this transform.
    /// </value>
    public Quaternion Rotation { get; set; }

    /// <summary>
    /// Gets or sets the scale.
    /// </summary>
    /// <value>
    /// The scale.
    /// </value>
    public Vector3 Scale { get; set; }

    /// <summary>
    /// Gets a normalized vector representing the Y-axis of the transform in world space.
    /// </summary>
    /// <value>
    /// The normalized vector representing the negative Y-axis of the transform in world space.
    /// </value>
    public Vector3 Up
    {
        get { return Vector3.Normalize(Vector3.Transform(Vector3.UnitY, this.Rotation)); }
    }

    /// <summary>
    /// Creates the transformation matrix for this transform.
    /// </summary>
    /// <returns>
    /// The newly create <see cref="Matrix4x4"/> that represents the transformation matrix for this transform.
    /// </returns>
    public Matrix4x4 CreateTransformationMatrix()
    {
        return Matrix4x4.CreateScale(this.Scale) *
               Matrix4x4.CreateFromQuaternion(this.Rotation) *
               Matrix4x4.CreateTranslation(this.Position);
    }

    /// <summary>
    /// Creates the view matrix for this transform.
    /// </summary>
    /// <param name="cameraUp">
    /// The camera up vector.
    /// </param>
    /// <returns>
    /// The newly created <see cref="Matrix4x4"/> that represents the view matrix for this transform.
    /// </returns>
    public Matrix4x4 CreateViewMatrix(Vector3 cameraUp)
    {
        return Matrix4x4.CreateLookAt(this.Position, this.Position + this.Forward, cameraUp);
    }

    /// <summary>
    /// Rotates this transform on the specified <paramref name="axis"/> by the specified <paramref name="radians"/>.
    /// </summary>
    /// <param name="axis">
    /// The axis to rotate.
    /// </param>
    /// <param name="radians">
    /// The radians that represents the amount to rotate by.
    /// </param>
    public void Rotate(Vector3 axis, float radians)
    {
        this.Rotation = Quaternion.CreateFromAxisAngle(axis, radians) * this.Rotation;
        this.Rotation = Quaternion.Normalize(this.Rotation);
    }

    /// <summary>
    /// Translates this transform in the specified <paramref name="direction"/> by the specified <paramref name="amount"/>.
    /// </summary>
    /// <param name="direction">
    /// The direction to translate.
    /// </param>
    /// <param name="amount">
    /// The amount to translate.
    /// </param>
    public void Translate(Vector3 direction, float amount)
    {
        this.Position += direction * amount;
    }
}
