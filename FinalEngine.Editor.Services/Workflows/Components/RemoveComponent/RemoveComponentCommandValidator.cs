namespace FinalEngine.Editor.Services.Workflows.Components.RemoveComponent
{
    using System;
    using FinalEngine.ECS;
    using FluentValidation;

    public class RemoveComponentCommandValidator : AbstractValidator<RemoveComponentCommand>
    {
        private readonly IEntityWorld world;

        public RemoveComponentCommandValidator(IEntityWorld world)
        {
            this.world = world ?? throw new ArgumentNullException(nameof(world));

            this.RuleFor(x => x.Entity)
                .Must(x =>
                {
                    return this.world.ContainsEntity(x);
                })
                .WithMessage("The entity doesn't exist in the current world.");

            this.RuleFor(x => x)
                .Must(x =>
                {
                    return !x.Entity.ContainsComponent(x.Component);
                })
                .WithMessage(x =>
                {
                    return $"The entity doesn't contain a component of type: '{x.Component.GetType()}'";
                });
        }
    }
}
