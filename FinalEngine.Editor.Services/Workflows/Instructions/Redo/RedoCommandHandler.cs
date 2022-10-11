namespace FinalEngine.Editor.Services.Workflows.Instructions.Redo
{
    using System;
    using FinalEngine.Editor.Services.Instructions;
    using MediatR;

    public class RedoCommandHandler : RequestHandler<RedoCommand>
    {
        private readonly IInstructionsManager instructionsManager;

        public RedoCommandHandler(IInstructionsManager instructionsManager)
        {
            this.instructionsManager = instructionsManager ?? throw new ArgumentNullException(nameof(instructionsManager));
        }

        protected override void Handle(RedoCommand request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            this.instructionsManager.Redo();
        }
    }
}
