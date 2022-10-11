namespace FinalEngine.Editor.Services.Workflows.Components.RemoveComponent
{
    using System;
    using FinalEngine.Editor.Services.Instructions;
    using FinalEngine.Editor.Services.Instructions.Components;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class RemoveComponentCommandHandler : RequestHandler<RemoveComponentCommand>
    {
        private readonly IInstructionsManager instructionsManager;

        private readonly ILogger<RemoveComponentCommandHandler> logger;

        private readonly IValidator<RemoveComponentCommand> validator;

        public RemoveComponentCommandHandler(
            ILogger<RemoveComponentCommandHandler> logger,
            IValidator<RemoveComponentCommand> validator,
            IInstructionsManager instructionsManager)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.instructionsManager = instructionsManager ?? throw new ArgumentNullException(nameof(instructionsManager));
        }

        protected override void Handle(RemoveComponentCommand request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            this.validator.ValidateAndThrow(request);

            var entity = request.Entity;
            var component = request.Component;

            this.logger.LogInformation($"Removing component of type: '{component.GetType()}' from entity with ID: '{entity.Identifier}'...");

            var instruction = new RemoveComponentInstruction(
                entity,
                component);

            this.instructionsManager.PerformInstruction(instruction);
            this.logger.LogInformation("Component removed from entity.");
        }
    }
}
