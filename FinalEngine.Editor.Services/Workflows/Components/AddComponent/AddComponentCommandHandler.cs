namespace FinalEngine.Editor.Services.Workflows.Components.AddComponent
{
    using System;
    using FinalEngine.Editor.Services.Instructions;
    using FinalEngine.Editor.Services.Instructions.Components;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class AddComponentCommandHandler : RequestHandler<AddComponentCommand>
    {
        private readonly IInstructionsManager instructionsManager;

        private readonly ILogger<AddComponentCommandHandler> logger;

        private readonly IValidator<AddComponentCommand> validator;

        public AddComponentCommandHandler(
            ILogger<AddComponentCommandHandler> logger,
            IValidator<AddComponentCommand> validator,
            IInstructionsManager instructionsManager)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
            this.instructionsManager = instructionsManager ?? throw new ArgumentNullException(nameof(instructionsManager));
        }

        protected override void Handle(AddComponentCommand request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            this.validator.ValidateAndThrow(request);

            var entity = request.Entity;
            var component = request.Component;

            this.logger.LogInformation($"Adding component of type: '{component.GetType()}' to entity with ID: '{entity.Identifier}'...");

            var instruction = new AddComponentInstruction(
                entity,
                component);

            this.instructionsManager.PerformInstruction(instruction);
            this.logger.LogInformation("Component added to entity.");
        }
    }
}
