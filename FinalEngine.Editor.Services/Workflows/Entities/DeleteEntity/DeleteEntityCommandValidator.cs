namespace FinalEngine.Editor.Services.Workflows.Entities.DeleteEntity
{
    using System;
    using FinalEngine.ECS;
    using FluentValidation;

    public class DeleteEntityCommandValidator : AbstractValidator<DeleteEntityCommand>
    {
        private readonly IEntityWorld world;

        public DeleteEntityCommandValidator(IEntityWorld world)
        {
            this.world = world ?? throw new ArgumentNullException(nameof(world));

            this.RuleFor(x => x.Entity)
                .Must(x =>
                {
                    return this.world.ContainsEntity(x);
                })
                .WithMessage("The entity doesn't exist in the current world.");
        }
    }
}
